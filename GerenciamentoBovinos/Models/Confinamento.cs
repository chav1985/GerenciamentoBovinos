using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentoBovinos.Models
{
    public class Confinamento
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("Bovino")]
        public long BovinoId { get; set; }
        public Bovino Bovino { get; set; }

        [Display(Name = "Produtos")]
        public List<Produto> LsProduto { get; set; }

        [Required]
        [Display(Name = "Data Inicial")]
        public DateTime DtInicial { get; set; }

        [Display(Name = "Data Final")]
        public DateTime DtFinal { get; set; }

        [Display(Name = "Custo total")]
        public decimal CustoTotal { get; set; }
    }
}