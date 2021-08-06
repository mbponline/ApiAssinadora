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
        public string Nome { get; set; }

        public long Id { get; set; }

        public byte[] Arquivo { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public long CertificadoId { get; set; }

        public Certificado Certificado { get; set; }

        public DateTime Data{get;set;}

        public Documento(string nome, string UserId, long CertificadoId, byte[] arquivo)
        {
            Nome = nome;
            this.UserId = UserId;
            this.CertificadoId = CertificadoId;
            Arquivo = arquivo;
            Data = DateTime.Now;
        }

    }

}
