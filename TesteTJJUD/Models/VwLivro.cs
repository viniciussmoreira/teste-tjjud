using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TesteTJJUD.Models
{
    [Table("vwLivros")]
    
    public class VwLivro
    {
        [Key]        
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public string Autor { get; set; }
        public string Assunto { get; set; }
        public string AnoPublicacao { get; set; }
        public decimal Valor { get; set; }
    }
}