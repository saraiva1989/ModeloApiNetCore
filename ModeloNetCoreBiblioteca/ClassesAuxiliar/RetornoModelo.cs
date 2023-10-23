using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModeloNetCoreBiblioteca.ClassesAuxiliar
{
    public class RetornoModelo
    {
        public bool Status { get; set; }
        public int Id { get; set; }
        public string IdMongo { get; set; } = string.Empty;
        public string Mensagem { get; set; } = string.Empty;
        public string Exception { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public dynamic Objeto { get; set; } = new object();
    }
}
