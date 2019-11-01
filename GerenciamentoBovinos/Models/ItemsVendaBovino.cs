using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentoBovinos.Models
{
    public class ItemsVendaBovino
    {
        [Key]
        public long Id { get; set; }

        public virtual VendaBovino VendaBovino { get; set; }
        [ForeignKey("VendaBovino")]
        public long VendaBovinoId { get; set; }

        public virtual Bovino Bovino { get; set; }
        [ForeignKey("Bovino")]
        public long BovinoId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal Peso { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal ValorUnitario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public decimal ValorTotal { get; set; }
    }
}