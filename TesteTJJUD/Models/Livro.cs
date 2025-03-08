using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TesteTJJUD.Models
{

    [Table("Livro")]
    public class Livro
    {
        public Livro()
        {
            LivroAutores = new List<LivroAutor>();
            LivroAssuntos = new List<LivroAssunto>();
        }
        [Key]
        public int Codl { get; set; }

        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Editora { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Apenas números")]
        public int Edicao { get; set; }

        [Required]
        public string AnoPublicacao { get; set; }
        [Required]
        public decimal Valor { get; set; }

        public ICollection<LivroAutor> LivroAutores { get; set; } = new List<LivroAutor>();

        public ICollection<LivroAssunto> LivroAssuntos { get; set; } = new List<LivroAssunto>();



    }
}