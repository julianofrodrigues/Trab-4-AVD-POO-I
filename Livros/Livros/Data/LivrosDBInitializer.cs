using DotLiquid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livros.Models;
using Livros.Data;


namespace Livros.Data
{
    public class LivrosDBInitializer
    {
        public static void Initialize(LivrosContext context)
        {
            context.Database.EnsureCreated();
        }

      
    }
}
