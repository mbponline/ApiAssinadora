using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CertificadoController : ControllerBase
{
    private readonly ICertificadoService _certserv;

    public CertificadoController(ICertificadoService CertificadoService)
    {
        _certserv = CertificadoService;
    }

    [HttpPost("Upload")]
    public async Task<ActionResult<CertificadoOutputPostDTO>> EnviaArquivo([FromForm] CertificadoInputPostDTO input)
    {
        var resp = await _certserv.Add(input, User.Identity.Name);
        return resp;
    }

    [HttpGet("Listar")]
    public async Task<ActionResult<List<CertificadoOutputGetDTO>>> Get()
    {
        return await _certserv.Get(User.Identity.Name);
    }

    [HttpPut("Atualizar")]
    public async Task<ActionResult<CertificadoOutputPutDTO>> Put([FromForm] CertificadoInputPutDTO input)
    {
        var resp = await _certserv.Update(input, User.Identity.Name);
        return resp;
    }

    [HttpDelete("Deletar{id}")]
    public async Task<ActionResult<CertificadoOutputDeleteDTO>> Delete(long id)
    {
        var resp = await _certserv.Deletar(id, User.Identity.Name);
        return resp;
    }





}

