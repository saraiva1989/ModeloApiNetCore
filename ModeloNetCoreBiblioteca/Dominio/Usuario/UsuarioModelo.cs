using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ModeloNetCoreBiblioteca.Dominio.Usuario
{
    public class UsuarioModelo
    {
        private string email = string.Empty;

        [BsonId]
        private ObjectId _id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        [Required]
        public string Email
        {
            get { return email; }
            set {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(value);
                if (!match.Success & _id.Pid == 0)
                    throw new Exception("Email inválido!");
                email = value;
            }
        }
        public string Senha { get; set; } = string.Empty;
        public int Nivel { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool UsuarioAtivo { get; set; }
        public DateTime DataAtivacao { get; set; }
        public string? ChaveAtivacao { get; set; }
    }
    public class TokenModelo
    {
        [BsonId]
        private ObjectId _id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ValidadeToken { get; set; }
        public string IP { get; set; } = string.Empty;
        public string NomeDispositivo { get; set; } = string.Empty;
        public string UsuarioEmail { get; set; } = string.Empty;
    }

}
