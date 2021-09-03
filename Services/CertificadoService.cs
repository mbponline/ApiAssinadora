using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiAssinadora.Models;
using DocSign.Domain.Util.Sign;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Net5_Api.Extensions;

public class CertificadoService : ICertificadoService
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly UserManager<ApplicationUser> _user;
    public CertificadoService(ApplicationDbContext context, IWebHostEnvironment environment, UserManager<ApplicationUser> user)
    {
        _context = context;
        _environment = environment;
        _user = user;
    }

    public async Task<CertificadoOutputPostDTO> Add(CertificadoInputPostDTO input, string user)
    {
        ApplicationUser usuario = await _user.FindByNameAsync(user);
        var userid = usuario.Id;

        string dir = _environment.ContentRootPath;
        string caminho = dir + "\\Arquivos\\Certificados\\_temp" + input.Arquivo.FileName;

        byte[] arquivo;
        using (var ms = new MemoryStream())
        {
            input.Arquivo.CopyTo(ms);
            arquivo = ms.ToArray();
            Cert certificado = new Cert(ms, input.Password);
        }
        var ext = System.IO.Path.GetExtension(input.Arquivo.FileName);

        var cert = new Certificado(input.Arquivo.FileName, arquivo, ext, input.Password, userid.ToString());
        _context.Certificados.Add(cert);
        await _context.SaveChangesAsync();

        var resp = new CertificadoOutputPostDTO(cert.Id, cert.Nome);
        return resp;
    }

    public async Task<CertificadoOutputListaDTO> Get(string user, int limit, int page, CancellationToken cancellationToken)
    {
        ApplicationUser usuario = await _user.FindByNameAsync(user);
        var userid = usuario.Id;

        var pagedModel = await _context.Certificados
                .AsNoTracking().Where(x => x.UserId == userid.ToString())
                .OrderBy(c => c.Id)
                .PaginateAsync(page, limit, cancellationToken);

        var CurrentPage = pagedModel.CurrentPage;
        var TotalPages = pagedModel.TotalPages;
        var TotalItems = pagedModel.TotalItems;
        var Items = pagedModel.Items.Select(c => new CertificadoOutputGetDTO(c.Id, c.Nome, c.Data)).ToList();

        CertificadoOutputListaDTO output = new CertificadoOutputListaDTO(CurrentPage, TotalPages, TotalItems, Items);

        return output;

        /*
        var lista = await _context.Certificados.Where(x => x.UserId == userid.ToString()).ToListAsync();
        List<CertificadoOutputGetDTO> listaout = new List<CertificadoOutputGetDTO>();

        foreach (Certificado cert in lista)
        {
            listaout.Add(new CertificadoOutputGetDTO(cert.Id, cert.Nome,cert.Data));
        }

        return listaout;
        */
    }

    public async Task<CertificadoOutputPutDTO> Update(CertificadoInputPutDTO input, string user)
    {
        ApplicationUser usuario = await _user.FindByNameAsync(user);
        var userid = usuario.Id;

        byte[] arquivo;
        using (var ms = new MemoryStream())
        {
            input.Arquivo.CopyTo(ms);
            arquivo = ms.ToArray();
            Cert certificado = new Cert(ms, input.Password);
        }
        var ext = System.IO.Path.GetExtension(input.Arquivo.FileName);

        var cert = await _context.Certificados.FirstOrDefaultAsync(c => c.UserId == userid.ToString() && c.Id == input.Id);

        cert.Nome = input.Arquivo.FileName;
        cert.Arquivo = arquivo;
        cert.Tipo = ext;
        cert.Senha = input.Password;
        cert.Data = DateTime.Now;

        _context.Certificados.Update(cert);
        await _context.SaveChangesAsync();

        var resp = new CertificadoOutputPutDTO(cert.Id, cert.Nome);
        return resp;

    }

    public async Task<CertificadoOutputDeleteDTO> Deletar(long id, string user)
    {
        ApplicationUser usuario = await _user.FindByNameAsync(user);
        var userid = usuario.Id;
        var cert = await _context.Certificados.FirstOrDefaultAsync(c => c.UserId == userid.ToString() && c.Id == id);

        _context.Certificados.Remove(cert);
        await _context.SaveChangesAsync();

        var resp = new CertificadoOutputDeleteDTO(cert.Id, cert.Nome);
        return resp;
    }





}