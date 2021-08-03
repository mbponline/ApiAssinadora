using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using ApiAssinadora.Models;
using Microsoft.EntityFrameworkCore;
using DocSign.Domain.Util.Sign;
using iTextSharp.text.pdf;

[ApiController]
[Route("[controller]")]
public class DocumentoController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public static IWebHostEnvironment _environment;
    public DocumentoController(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    [HttpPost("Upload")]
    public async Task<ActionResult<DocumentoOutputPostDTO>> EnviaArquivo([FromForm] DocumentoInputPostDTO documento)
    {
        IFormFile arquivo = documento.Arquivo;
        var doc = new Documento();

        if (arquivo.Length > 0)
        {
            try
            {
                string dir = _environment.ContentRootPath;
                string caminho = dir + "\\Arquivos\\Documentos\\Assinar\\" + arquivo.FileName;
                if (!Directory.Exists(dir + "\\Arquivos\\Documentos\\Assinar\\"))
                {
                    Directory.CreateDirectory(dir + "\\Arquivos\\Documentos\\Assinar\\");
                }
                _context.Documentos.Add(doc);
                doc.Caminho = dir + "\\Arquivos\\Documentos\\Assinar\\" + doc.Id + ".pdf";
                _context.Documentos.Update(doc);
                await _context.SaveChangesAsync();
                var output = new DocumentoOutputPostDTO(doc.Id,doc.Caminho);

                using (FileStream filestream = System.IO.File.Create(doc.Caminho))
                {
                    await arquivo.CopyToAsync(filestream);
                    filestream.Flush();
                    await Task.Delay(100);
                    await filestream.DisposeAsync();
                    filestream.Close();
                }
                return Ok(output);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message.ToString());
            }
        }
        else
        {
            return Conflict("Erro de Upload"); ;
        }
    }
    [HttpGet("Assinar/{id}")]
    [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
    public async Task<IActionResult> GetAssinar(long id)
    {
        Documento d = await _context.Documentos.FindAsync(id);
        string docpath = _environment.ContentRootPath + "\\Arquivos\\Documentos\\Assinar\\" + id +  ".pdf";
        string asspath = _environment.ContentRootPath + "\\Arquivos\\Documentos\\Assinado\\" + id + ".pdf";
        string certpath = _environment.ContentRootPath + "\\Arquivos\\Certificados\\" + "Certificado.pfx";
        string certsenha = "22232425";

        if (!Directory.Exists(_environment.ContentRootPath + "\\Arquivos\\Documentos\\Assinado\\"))
        {
            Directory.CreateDirectory(_environment.ContentRootPath + "\\Arquivos\\Documentos\\Assinado\\");
        }

        Cert myCert = null;
        try
        {
            myCert = new Cert(certpath, certsenha);
        }
        catch (Exception ex)
        {
            return Conflict(ex.ToString());
        }

        PdfReader reader = new PdfReader(docpath);

        MetaData MyMD = new MetaData();
        MyMD.Info = reader.Info; ;
        reader.Close();

        PDFSigner pdfs = new PDFSigner(docpath, asspath, myCert, MyMD);
        pdfs.Sign("Teste", "Teste", "Teste", true);
        await Task.Delay(1000);
        if (System.IO.File.Exists(asspath))
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            var file = System.IO.File.ReadAllBytes(asspath);
            System.IO.File.Delete(docpath);
            return File(file, "application/octet-stream", "Assinado.pdf");
        }
        else
            return BadRequest("Errou Aqui");

    }

}