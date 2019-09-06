using System.ComponentModel.DataAnnotations;

namespace GerenciamentoBovinos.Models
{
    public class Motorista : Pessoa
    {
        [Key]

        public long Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string CNH { get; set; }

        [MaxLength(200, ErrorMessage = "A {0} deve ter no máximo {1} caracteres")]
        public string Descricao { get; set; }

    }
}