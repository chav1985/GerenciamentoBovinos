using System;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoBovinos.Models
{
    public class Fornecedor : Pessoa
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(30, ErrorMessage = "A {0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Inscrição Estadual")]
        public String InscricaoEstadual { get; set; }
    }
}