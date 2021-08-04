using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public class DocumentoInputPostDTO
{
    public IFormFile Arquivo { get; set; }

    public long CertId { get; set; }

}

public class DocumentoOutputPostDTO
{
    public string Nome { get; set; }
    public long Id { get; set; }
    public byte[] Arquivo { get; set; }

    public DocumentoOutputPostDTO(long id, string nome, byte[] arquivo)
    {
        this.Nome = nome;
        this.Id = id;
        this.Arquivo = arquivo;
    }
}

public class DocumentoOutputUrlDTO
{
    public string Nome { get; set; }
    public long Id { get; set; }
    public string Url { get; set; }

    public DocumentoOutputUrlDTO(long id, string nome, string url)
    {
        this.Nome = nome;
        this.Id = id;
        this.Url = url;
    }
}

public class DocumentoOutputGetDTO
{

    public string Nome { get; set; }
    public long Id { get; set; }

    public string Certificado { get; set; }

    public DocumentoOutputGetDTO(long id,string nome,string certificado)
    {
        Id = id;
        Nome = nome;
        Certificado = certificado;
    }

}



