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
        [Display(Name = "Data de Compra")]
        public DateTime DtCompra { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Prazo de Entrega")]
        public DateTime PrazoEntrega { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public List<Produto> LsProdutos { get; set; }
    }
}