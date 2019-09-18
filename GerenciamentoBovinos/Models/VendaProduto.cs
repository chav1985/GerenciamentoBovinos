using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoBovinos.Models
{
    public class VendaProduto
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Data de Venda")]
        public DateTime DtVenda { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Prazo de Entrega")]
        public DateTime PrazoEntrega { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Margem de Venda")]
        public int MargemVenda { get; set; }

        public virtual List<ItemsVendaProduto> Items { get; set; }
    }
}