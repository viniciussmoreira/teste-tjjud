using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TesteTJJUD.Models
{
    [Table("Assunto")]
    public class Assunto
    {
        [Key]
        public int CodAs { get; set; }

        [Required, StringLength(20)]
        public string Descricao { get; set; }

        
        public ICollection<LivroAssunto> LivroAssuntos { get; set; } = new List<LivroAssunto>();
    }
}