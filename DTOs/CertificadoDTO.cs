using System;
using Microsoft.AspNetCore.Http;

public class CertificadoInputPostDTO
{
    public IFormFile Arquivo { get; set; }
    public string Password { get; set; }

}

public class CertificadoOutputPostDTO
{

    public string Nome { get; set; }
    public long Id { get; set; }

    public CertificadoOutputPostDTO(long id, string nome)
    {
        Id = id;
        Nome = nome;
    }

}

public class CertificadoOutputGetDTO
{

    public string Nome { get; set; }
    public long Id { get; set; }
    public string Data { get; set; }

    public CertificadoOutputGetDTO(long id, string nome,DateTime data)
    {
        Id = id;
        Nome = nome;
        Data = data.ToString("dd/MM/yyyy HH:mm:ss");
    }

}

public class CertificadoInputPutDTO
{
    public IFormFile Arquivo { get; set; }
    public string Password { get; set; }
    public long Id { get; set; }

}

public class CertificadoOutputPutDTO
{
    public string Nome { get; set; }
    public long Id { get; set; }

    public CertificadoOutputPutDTO(long id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class CertificadoOutputDeleteDTO
{
    public string Nome { get; set; }
    public long Id { get; set; }

    public CertificadoOutputDeleteDTO(long id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}