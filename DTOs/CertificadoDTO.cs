using Microsoft.AspNetCore.Http;

public class CertificadoInputPostDTO
{
    public IFormFile Arquivo { get; set; }
    public string Password { get; set; }

}

public class CertificadoOutputPostDTO
{

    public string Certificado { get; set; }
    public long Id { get; set; }

    public CertificadoOutputPostDTO(long id, string certificado)
    {
        Id = id;
        Certificado = certificado;
    }

}

public class CertificadoOutputGetDTO
{

    public string Certificado { get; set; }
    public long Id { get; set; }

    public CertificadoOutputGetDTO(long id)
    {
        Id = id;
    }

}