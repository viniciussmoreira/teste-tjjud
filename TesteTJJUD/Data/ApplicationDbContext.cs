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

        //public ApplicationDbContext() : base("TJConecctionString_desenv")
        //{
        //    Database.SetInitializer<ApplicationDbContext>(null); // Desativa a inicialização automática, sem migration
        //}

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Assunto> Assuntos { get; set; }
        public DbSet<LivroAutor> LivroAutores { get; set; }
        public DbSet<LivroAssunto> LivroAssuntos { get; set; }

        public DbSet<VwLivro> VwLivros { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LivroAutor>()
                .HasRequired(la => la.Livro)
                .WithMany(l => l.LivroAutores)
                .HasForeignKey(la => la.Livro_Codl);


            //modelBuilder.Entity<LivroAutor>()
            //    .HasRequired(la => la.Autor)
            //    .WithMany()
            //    .HasForeignKey(la => la.Autor_CodAu);

            //modelBuilder.Entity<LivroAssunto>()
            //    .HasRequired(la => la.Livro)
            //    .WithMany(l => l.LivroAssuntos)
            //    .HasForeignKey(la => la.Livro_Codl);

            //modelBuilder.Entity<LivroAssunto>()
            //    .HasRequired(la => la.Assunto)
            //    .WithMany()
            //    .HasForeignKey(la => la.Assunto_CodxAs);
        }


    }


}