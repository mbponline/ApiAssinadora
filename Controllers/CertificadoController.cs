using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Threading;

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
    public async Task<ActionResult<CertificadoOutputListaDTO>> Get(CancellationToken cancellationToken, int limit = 5, int page = 1)
    {
        var output =await _certserv.Get(User.Identity.Name,limit, page, cancellationToken);
        return Ok(output);
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

