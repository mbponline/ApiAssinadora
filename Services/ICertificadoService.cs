using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiAssinadora.Models;

public interface ICertificadoService
{
    Task<CertificadoOutputPostDTO> Add(CertificadoInputPostDTO input);


}