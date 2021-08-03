using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text;
using System;
using ApiAssinadora.Models;
using Microsoft.EntityFrameworkCore;

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
        var resp = await _certserv.Add(input);
        return resp;
    }




}

