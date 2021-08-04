using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiAssinadora.Models;

public interface IDocumentoService
{
    Task<DocumentoOutputPostDTO> Add(DocumentoInputPostDTO input,string user);

    Task<List<DocumentoOutputGetDTO>> Get(string user);


}