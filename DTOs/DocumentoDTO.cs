using System;
using System.Collections.Generic;
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
    public string Data { get; set; }
    public DocumentoOutputPostDTO(long id, string nome, DateTime data)
    {
        this.Nome = nome;
        this.Id = id;
        this.Data = data.ToString("dd/MM/yyyy HH:mm:ss");
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
    public string Data { get; set; }

    public DocumentoOutputGetDTO(long id, string nome, string certificado, DateTime data)
    {
        Id = id;
        Nome = nome;
        Certificado = certificado;
        Data = data.ToString("dd/MM/yyyy HH:mm:ss");
    }

}

public class DocumentoOutputGetDownloadDTO
{
    public byte[] Arquivo { get; set; }
    public string Nome { get; set; }

    public DocumentoOutputGetDownloadDTO(byte[] arquivo, string nome)
    {
        Arquivo = arquivo;
        Nome = nome;
    }


}

public class DocumentoOutputPostXMLDTO
{
    public long Id { get; set; }
    public string Nome { get; set; }

    public DocumentoOutputPostXMLDTO(long id, string nome)
    {
        Id = id;
        Nome = nome;
    }


}

public class DocumentoInputPostXMLDTO
{
    public IFormFile Arquivo { get; set; }

    public long CertId { get; set; }

}

public class DocumentoOutputListaDTO
{
    public DocumentoOutputListaDTO(int currentpage, int totalitems, int totalpages, List<DocumentoOutputGetDTO> items)
    {
        CurrentPage = currentpage;
        TotalItems = totalitems;
        TotalPages = totalpages;
        Items = items;
    }

    public int CurrentPage { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public List<DocumentoOutputGetDTO> Items { get; set; }

}




