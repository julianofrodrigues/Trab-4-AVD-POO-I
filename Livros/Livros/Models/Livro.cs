using Livros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livros.Models
{
    public class Livro
    {
        public long? LivroId { get; set; }
        public string Ano { get; set; }
        public string ISBN { get; set; }
        public string Titulo { get; set; }

        public long? AutorId { get; set; }
        public Autor Autor { get; set; }

        public long? CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}
