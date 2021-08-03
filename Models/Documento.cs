using DocSign.Domain.Util.Sign;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiAssinadora.Models
{
    public class Documento
    {
        [JsonIgnore]public string Caminho { get; set; }

        [JsonIgnore]public long Id { get; set; }

        [NotMapped] public IFormFile Arquivo { get; set; }

        public Documento(string caminho)
        {
            Caminho = caminho;
        }
        public Documento(){}
    }
    
}
