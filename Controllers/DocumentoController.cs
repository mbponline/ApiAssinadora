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
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class DocumentoController : ControllerBase
{
    private readonly IDocumentoService _docserv;

    public DocumentoController(IDocumentoService DocumentoService)
    {
        _docserv = DocumentoService;
    }


    [HttpPost("Assinar")]
    public async Task<ActionResult<dynamic>> EnviaArquivo([FromForm] DocumentoInputPostDTO input)
    {
        var resp = await _docserv.Add(input, User.Identity.Name);
        return File(resp.Arquivo, "application/octet-stream", resp.Nome);
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<dynamic>> GetAll()
    {
        var resp = await _docserv.Get(User.Identity.Name);
        return resp;
    }

}