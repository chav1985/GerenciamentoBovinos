using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentoBovinos.Models
{
    public class ItemsVendaProduto
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("VendaProduto")]
        public long VendaProdutoId { get; set; }
        public VendaProduto VendaProduto { get; set; }

        [ForeignKey("Produto")]
        public long ProdutoId { get; set; }
        public Produto Produto { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public int Qtd { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal ValorUnitario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal ValorTotal { get; set; }
    }
}