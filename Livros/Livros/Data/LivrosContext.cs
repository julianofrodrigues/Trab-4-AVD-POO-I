using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livros.Models;
using Microsoft.EntityFrameworkCore;

namespace Livros.Data
{
    public class LivrosContext : DbContext
    {
        public LivrosContext(DbContextOptions<LivrosContext> options) : base(options)
        {

        }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }

    }
}
