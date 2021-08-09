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
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using System.Threading;

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

    [HttpPost("Enviar/Pdf")]
    public async Task<ActionResult<dynamic>> EnviaArquivo([FromForm] DocumentoInputPostDTO input)
    {
        var resp = await _docserv.EnviarPDF(input, User.Identity.Name);
        return Ok(resp);
    }

    [HttpPost("Enviar/Xml/")]
    public async Task<ActionResult<DocumentoOutputPostXMLDTO>> TesteXML([FromForm] DocumentoInputPostXMLDTO input)
    {
        return await _docserv.EnviarXML(input, User.Identity.Name);
    }


    [HttpGet("ListarDocumentos")]
    public async Task<ActionResult<CertificadoOutputListaDTO>> GetAll(CancellationToken cancellationToken, int limit = 5, int page = 1)
    {
        var output = await _docserv.Get(User.Identity.Name, limit, page, cancellationToken);
        return Ok(output);
    }

    [HttpGet("Download/{id}")]
    public async Task<ActionResult<dynamic>> Download(long id)
    {
        var resp = await _docserv.Download(User.Identity.Name, id);
        return File(resp.Arquivo, "application/octet-stream", resp.Nome);
    }
    /*
    [HttpGet("Download/Url/{id}")]
    public async Task<ActionResult<DocumentoOutputUrlDTO>> DownloadUrl(long id)
    {
        var token = await Request.HttpContext.GetTokenAsync("access_token");
        string path = Request.HttpContext.Request.Host.Value;
        var resp = await _docserv.DownloadUrl(User.Identity.Name, id, path, token);
        return resp;
    }
    */
}