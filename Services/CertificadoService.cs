using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiAssinadora.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        string caminho = dir + "\\Arquivos\\Certificados\\" + input.Arquivo.FileName;

        if (!Directory.Exists(dir + "\\Arquivos\\Certificados\\"))
        {
            Directory.CreateDirectory(dir + "\\Arquivos\\Certificados\\");
        }

        byte[] arquivo;
        using (var ms = new MemoryStream())
        {
            input.Arquivo.CopyTo(ms);
            arquivo = ms.ToArray();
            //arquivo = Convert.ToBase64String(fileBytes);
        }

        var ext = System.IO.Path.GetExtension(input.Arquivo.FileName);


        var cert = new Certificado(arquivo, ext, input.Password, userid.ToString());
        _context.Certificados.Add(cert);
        await _context.SaveChangesAsync();

        File.WriteAllBytes(caminho, cert.Arquivo);

        var resp = new CertificadoOutputPostDTO(cert.Id, cert.Tipo);
        return resp;
    }

    public async Task<List<CertificadoOutputGetDTO>> Get(string user)
    {
        ApplicationUser usuario = await _user.FindByNameAsync(user);
        var userid = usuario.Id;
        var lista = await _context.Certificados.Where(x => x.UserId == userid.ToString()).ToListAsync();
        List<CertificadoOutputGetDTO> listaout = new List<CertificadoOutputGetDTO>();

        foreach (Certificado cert in lista)
        {
            listaout.Add(new CertificadoOutputGetDTO(cert.Id));
        }

        return listaout;
    }



}