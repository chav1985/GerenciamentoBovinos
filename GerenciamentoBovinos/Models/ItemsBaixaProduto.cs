using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentoBovinos.Models
{
    public class ItemsBaixaProduto
    {
        [Key]
        public long Id { get; set; }

        public virtual BaixaProduto BaixaProduto { get; set; }
        [ForeignKey("BaixaProduto")]
        public long? BaixaProdutoId { get; set; }

        public virtual Produto Produto { get; set; }
        [ForeignKey("Produto")]
        public long ProdutoId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public int Qtd { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal ValorUnitario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal ValorTotal { get; set; }
    }
}