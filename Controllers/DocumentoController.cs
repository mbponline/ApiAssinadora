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

    [HttpGet("Download/{id}")]
    public async Task<ActionResult<dynamic>> Download(long id)
    {
        var resp = await _docserv.Download(User.Identity.Name, id);
        return File(resp.Arquivo, "application/octet-stream", resp.Nome);
    }

    [HttpGet("Download/Url/{id}")]
    public async Task<ActionResult<DocumentoOutputUrlDTO>> DownloadUrl(long id)
    {
        var token = await Request.HttpContext.GetTokenAsync("access_token");
        string path = Request.HttpContext.Request.Host.Value;
        var resp = await _docserv.DownloadUrl(User.Identity.Name, id, path, token);
        return resp;
    }

    [HttpPost("Teste/Xml/")]
    public async Task<ActionResult<DocumentoOutputPostXMLDTO>> TesteXML([FromForm] DocumentoInputPostDTO input)
    {
        return await _docserv.TesteXML(input, User.Identity.Name);
    }



}