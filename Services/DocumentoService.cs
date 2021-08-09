using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApiAssinadora.Models;
using DocSign.Domain.Util.Sign;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Net5_Api.Extensions;
using System.Threading;

public class DocumentoService : IDocumentoService
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly UserManager<ApplicationUser> _user;
    public DocumentoService(ApplicationDbContext context, IWebHostEnvironment environment, UserManager<ApplicationUser> user)
    {
        _context = context;
        _environment = environment;
        _user = user;
    }

    public async Task<DocumentoOutputPostDTO> EnviarPDF(DocumentoInputPostDTO input, string user)
    {
        ApplicationUser usuario = await _user.FindByNameAsync(user);
        var userid = usuario.Id;
        var certid = input.CertId;

        byte[] arquivopdf;

        using (var ms = new MemoryStream())
        {
            input.Arquivo.CopyTo(ms);
            arquivopdf = ms.ToArray();
        }

        var certificado = await _context.Certificados.FirstOrDefaultAsync(c => c.UserId == userid.ToString() && c.Id == certid);
        MemoryStream CertStream = new MemoryStream(certificado.Arquivo);

        Cert myCert = new Cert(CertStream, certificado.Senha);

        var reader = new PdfReader(arquivopdf);

        MetaData MyMD = new MetaData();
        MyMD.Info = reader.Info;

        PDFSigner pdfs = new PDFSigner(reader, "", myCert, MyMD);

        var pdfstream = pdfs.SignReader("Teste", "Teste", "Teste", false);

        byte[] arquivo = pdfstream.ToArray();

        var documento = new Documento(input.Arquivo.FileName, usuario.Id, certificado.Id, arquivo);

        _context.Documentos.Add(documento);
        await _context.SaveChangesAsync();

        var output = new DocumentoOutputPostDTO(documento.Id, documento.Nome, documento.Data);

        return output;
    }

    public async Task<DocumentoOutputListaDTO> Get(string user, int limit, int page, CancellationToken cancellationToken)
    {
        ApplicationUser usuario = await _user.FindByNameAsync(user);
        var userid = usuario.Id;

        var pagedModel = await _context.Documentos
                .AsNoTracking()
                .Include(c => c.Certificado)
                .Where(x => x.UserId == userid.ToString())
                .OrderBy(x => x.Id)
                .PaginateAsync(page, limit, cancellationToken);

        var CurrentPage = pagedModel.CurrentPage;
        var TotalPages = pagedModel.TotalPages;
        var TotalItems = pagedModel.TotalItems;
        var Items = pagedModel.Items.Select(c => new DocumentoOutputGetDTO(c.Id, c.Nome, c.Certificado.Nome, c.Data)).ToList();

        DocumentoOutputListaDTO output = new DocumentoOutputListaDTO(CurrentPage, TotalPages, TotalItems, Items);

        return output;
    }

    public async Task<DocumentoOutputGetDownloadDTO> Download(string user, long id)
    {
        ApplicationUser usuario = await _user.FindByNameAsync(user);
        var userid = usuario.Id;
        var documento = await _context.Documentos.FirstOrDefaultAsync(c => c.UserId == userid.ToString() && c.Id == id);
        var output = new DocumentoOutputGetDownloadDTO(documento.Arquivo, documento.Nome);
        return (output);
    }

    public async Task<DocumentoOutputUrlDTO> DownloadUrl(string user, long id, string path, string token)
    {
        ApplicationUser usuario = await _user.FindByNameAsync(user);
        var userid = usuario.Id;
        var documento = await _context.Documentos.FirstOrDefaultAsync(c => c.UserId == userid.ToString() && c.Id == id);

        string dir = _environment.ContentRootPath;
        string caminho = dir + "\\Arquivos\\Documentos\\" + documento.Nome;

        if (!Directory.Exists(dir + "\\Arquivos\\Documentos\\"))
        {
            Directory.CreateDirectory(dir + "\\Arquivos\\Documentos\\");
        }

        using (MemoryStream ms = new MemoryStream(documento.Arquivo))
        {
            using (FileStream fs = new FileStream(caminho, FileMode.OpenOrCreate))
            {
                ms.WriteTo(fs);
            }

        }
        Criptografia cripto = new Criptografia();
        string ID = cripto.Criptografar(usuario.UserName, false);
        //string DOC = cripto.Criptografar(documento.Id.ToString(),false);

        var urlDownload = path + "/Arquivos/Documentos" + "?t=" + ID + "&i=";// + DOC;
        Console.WriteLine(ID);
        Console.WriteLine(cripto.Descriptografar(ID, false));
        var output = new DocumentoOutputUrlDTO(documento.Id, documento.Nome, urlDownload);
        return output;
    }
    public async Task<DocumentoOutputPostXMLDTO> EnviarXML(DocumentoInputPostXMLDTO input, string user)
    {
        ApplicationUser usuario = await _user.FindByNameAsync(user);
        var userid = usuario.Id;

        var certificado = await _context.Certificados.FirstOrDefaultAsync(c => c.UserId == userid.ToString() && c.Id == input.CertId);
        XMLSigner Signer = new XMLSigner();
        byte[] xml;

        using (var ms = new MemoryStream())
        {
            input.Arquivo.CopyTo(ms);
            xml = ms.ToArray();
        }

        byte[] xmlsig = Signer.SingXML(certificado.Arquivo, certificado.Senha, xml);

        var documento = new Documento(input.Arquivo.FileName, usuario.Id, certificado.Id, xmlsig);
        _context.Documentos.Add(documento);
        await _context.SaveChangesAsync();

        var output = new DocumentoOutputPostXMLDTO(documento.Id, documento.Nome);
        return (output);
    }
}