using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TesteTJJUD.Models;

namespace TesteTJJUD.Data
{
    
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("TJConecctionString")
        {
            Database.SetInitializer<ApplicationDbContext>(null); // Desativa a inicialização automática, sem migration
        }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Assunto> Assuntos { get; set; }
        public DbSet<LivroAutor> LivroAutores { get; set; }
        public DbSet<LivroAssunto> LivroAssuntos { get; set; }
    }


}