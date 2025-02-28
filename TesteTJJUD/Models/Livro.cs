using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TesteTJJUD.Models
{

	[Table("Livro")]
	public class Livro
	{
		[Key]
		public int Codl { get; set; }

		[Required]
		public string Titulo { get; set; }
        [Required]
        public string Editora { get; set; }
        [Required]
        public int Edicao { get; set; }
        [Required]
        public string AnoPublicacao { get; set; }
        [Required]
        public decimal Valor { get; set; }

		public ICollection<LivroAutor> LivroAutores { get; set; }
		public ICollection<LivroAssunto> LivroAssuntos{ get; set; }

    }
}