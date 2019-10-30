using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentoBovinos.Models
{
    public class VendaBovino
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Data de Venda")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DtVenda { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Prazo de Entrega")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PrazoEntrega { get; set; }

        public virtual Cliente Cliente { get; set; }
        [ForeignKey("Cliente")]
        public long ClienteId { get; set; }

        [Display(Name = "Total Pedido")]
        public decimal TotalPedido { get; set; }

        public virtual List<Confinamento> Items { get; set; }
    }
}