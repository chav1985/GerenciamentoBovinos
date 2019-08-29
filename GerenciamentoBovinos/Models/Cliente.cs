using System.ComponentModel.DataAnnotations;

namespace GerenciamentoBovinos.Models
{
    public class Cliente : Pessoa
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(30, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Inscrição Estadual")]
        public string InscricaoEstadual { get; set; }

    }
}