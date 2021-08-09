using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiAssinadora.Models;

public interface IDocumentoService
{
    Task<DocumentoOutputPostDTO> EnviarPDF(DocumentoInputPostDTO input, string user);
    Task<DocumentoOutputListaDTO> Get(string user, int limit, int page, CancellationToken cancellationToken);
    Task<DocumentoOutputGetDownloadDTO> Download(string user, long id);
    Task<DocumentoOutputUrlDTO> DownloadUrl(string user, long id, string path, string token);
    Task<DocumentoOutputPostXMLDTO> EnviarXML(DocumentoInputPostXMLDTO input, string user);


}