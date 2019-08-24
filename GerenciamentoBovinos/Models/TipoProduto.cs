using System.ComponentModel.DataAnnotations;

namespace GerenciamentoBovinos.Models
{
    public class TipoProduto
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MinLength(3, ErrorMessage = "O {0} deve ter no mínimo {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }
        [DataType(DataType.MultilineText)]
        [MaxLength(50, ErrorMessage = "A {0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}