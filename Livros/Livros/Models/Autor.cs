using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livros.Models
{
    public class Autor
    {
        public long? AutorId { get; set; }
        public string Name { get; set; }
        public string Nacionalidade { get; set; }

        public virtual ICollection<Livro> Livros { get; set; }
    }
}
