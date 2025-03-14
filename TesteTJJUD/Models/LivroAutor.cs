﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TesteTJJUD.Models
{
    [Table("Livro_Autor")]
    public class LivroAutor
    {        
        [Key]
        public int Id { get; set; }

        [ForeignKey("Livro")]        
        public int Livro_Codl { get; set; }
        public virtual Livro Livro { get; set; }

        [ForeignKey("Autor")]        
        public int Autor_CodAu { get; set; }
        public virtual Autor Autor { get; set; }
    }
}