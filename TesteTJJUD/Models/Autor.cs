using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TesteTJJUD.Models
{

    [Table("Autor")]
    public class Autor
    {
        [Key]
        public int CodAu { get; set; }

        [Required, StringLength(40)]
        public string Nome { get; set; }
        //1 Autor N Livros
        public ICollection<LivroAutor> LivroAutores { get; set; }
    }
}