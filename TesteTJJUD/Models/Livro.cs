using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Key]
        public int Codl { get; set; }

        [Required]
        [MaxLength(40)]
        public string Titulo { get; set; }

        [Required]
        [MaxLength(40)]
        public string Editora { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Apenas números")]
        public int Edicao { get; set; }

        [Required]
        [MaxLength(4)]
        public string AnoPublicacao { get; set; }
        
        [Required]
        [DefaultValue(0)]
        [DisplayFormat(DataFormatString = "{0:C}",ApplyFormatInEditMode = false )]
        
        public decimal Valor { get; set; }

        public ICollection<LivroAutor> LivroAutores { get; set; } = new List<LivroAutor>();

        public ICollection<LivroAssunto> LivroAssuntos { get; set; } = new List<LivroAssunto>();



    }
}