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

    public async Task<DocumentoOutputPostDTO> Add(DocumentoInputPostDTO input, string user)
    {
        ApplicationUser usuario = await _user.FindByNameAsync(user);
        var userid = usuario.Id;
        var certid = input.CertId;

        string dir = _environment.ContentRootPath;
        string caminho = dir + "\\Arquivos\\Documentos\\Assinados\\" + input.Arquivo.FileName;

        if (!Directory.Exists(dir + "\\Arquivos\\Documentos\\Assinados\\"))
        {
            Directory.CreateDirectory(dir + "\\Arquivos\\Documentos\\Assinados\\");
        }

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

        PDFSigner pdfs = new PDFSigner(reader, caminho, myCert, MyMD);

        var pdfstream = pdfs.SignReader("Teste", "Teste", "Teste", true);

        // using (var stream = new FileStream(caminho, FileMode.CreateNew))
        //{
        //   pdfstream.WriteTo(stream);
        // }

        byte[] arquivo = pdfstream.ToArray();

        var documento = new Documento(input.Arquivo.FileName, usuario.Id, certificado.Id, arquivo);

        _context.Documentos.Add(documento);
        await _context.SaveChangesAsync();

        var output = new DocumentoOutputPostDTO(documento.Id, documento.Nome, documento.Arquivo);

        return output;
    }

    public async Task<List<DocumentoOutputGetDTO>> Get(string user)
    {
        ApplicationUser usuario = await _user.FindByNameAsync(user);
        var userid = usuario.Id;
        var lista = await _context.Documentos.Include(d => d.Certificado).Where(x => x.UserId == userid.ToString()).ToListAsync();
        List<DocumentoOutputGetDTO> listaout = new List<DocumentoOutputGetDTO>();

        foreach (var doc in lista)
        {
            listaout.Add(new DocumentoOutputGetDTO(doc.Id,doc.Nome,doc.Certificado.Nome));
        }

        return listaout;
    }

}