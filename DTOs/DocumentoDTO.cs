using Microsoft.AspNetCore.Http;

public class DocumentoInputPostDTO {

     public IFormFile Arquivo { get; set;}

     public DocumentoInputPostDTO(IFormFile arquivo){
          Arquivo = arquivo;
     }

     public DocumentoInputPostDTO(){}
}

public class DocumentoOutputPostDTO {
    public long Id { get; set; }
    public string Caminho{get;set;}

    
    public DocumentoOutputPostDTO(long id,string caminho) {
        Id = id;
        Caminho = caminho;
    }
}

public class DocumentoInputPutDTO{
     public string Caminho{get;set;}

     public DocumentoInputPutDTO(string caminho)
     {
          Caminho = caminho;
     }

}

public class DocumentoInputGetDTO
{
     public long Id;

     public DocumentoInputGetDTO(long id)
     {
          Id = id;
     }
}

