using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TesteTJJUD.Models
{
    [Table("Livro_Assunto")]
    public class LivroAssunto
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Livro")]
        public int Livro_Codl { get; set; }
        public Livro Livro { get; set; }

        [ForeignKey("Assunto")]
        public int Assunto_CodAs { get; set; }
        public Assunto Assunto { get; set; }
    }
}