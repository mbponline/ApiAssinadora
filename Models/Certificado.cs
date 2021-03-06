
using DocSign.Domain.Util.Sign;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAssinadora.Models
{
    public class Certificado
    {
        public Certificado(string nome, byte[] arquivo, string tipo, string senha, string UserId)
        {
            this.Nome = nome;
            this.Arquivo = arquivo;
            this.Tipo = tipo;
            this.Senha = senha;
            this.UserId = UserId;
            this.Data = DateTime.Now;
        }
        public long Id { get; set; }
        public string Nome { get; set; }
        public byte[] Arquivo { get; set; }
        public string Tipo { get; set; }
        public string Senha { get; set; }
        [ForeignKey("UserId")]
        [Required]
        public string UserId { get; set; }
        public DateTime Data { get; set; }
    }
}