using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ApiAssinadora.Models;
using Microsoft.AspNetCore.Hosting;

public class CertificadoService : ICertificadoService
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;
    public CertificadoService(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<CertificadoOutputPostDTO> Add(CertificadoInputPostDTO input)
    {

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


        var cert = new Certificado(arquivo, ext, input.Password);
        _context.Certificados.Add(cert);
        await _context.SaveChangesAsync();

        File.WriteAllBytes(caminho, cert.Arquivo);

        var resp = new CertificadoOutputPostDTO(cert.Id, cert.Tipo);
        return resp;
    }



}