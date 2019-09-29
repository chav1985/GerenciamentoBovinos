using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentoBovinos.Models
{
    public class Produto
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MinLength(3, ErrorMessage = "O {0} deve ter no mínimo {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Produto")]
        public string NomeProduto { get; set; }

        public virtual TipoProduto TipoProduto { get; set; }
        [ForeignKey("TipoProduto")]
        public long TipoProdutoId { get; set; }

        public virtual Fornecedor Fornecedor { get; set; }
        [ForeignKey("Fornecedor")]
        public long FornecedorId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Valor Unitário")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public DateTime Validade { get; set; }

        [MaxLength(200, ErrorMessage = "A {0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Quantidade")]
        public int Qtd { get; set; }
    }
}