using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace livro.Models
{
    public class Livros
    {
        public long? LivroId { get; set; }

        public string Nome { get; set; }
        public string Ano { get; set; }
        public string ISBN { get; set; }
        public string Titulo { get; set; }
    }
}
