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
        var resp = await _certserv.Add(input,User.Identity.Name);
        return resp;
    }

    [HttpGet]
    public async Task<ActionResult<List<CertificadoOutputGetDTO>>> Get()
    {
        return await _certserv.Get(User.Identity.Name);
    }




}

