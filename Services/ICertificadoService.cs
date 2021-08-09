using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApiAssinadora.Models;

public interface ICertificadoService
{
    Task<CertificadoOutputPostDTO> Add(CertificadoInputPostDTO input, string user);

    Task<CertificadoOutputListaDTO> Get(string user, int limit, int page, CancellationToken cancellationToken);

    Task<CertificadoOutputPutDTO> Update(CertificadoInputPutDTO input, string user);

    Task<CertificadoOutputDeleteDTO> Deletar(long id, string user);


}