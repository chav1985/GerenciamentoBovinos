using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GerenciamentoBovinos.Models
{
    public class Veterinario : Pessoa
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(30, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string CRMV { get; set; }
    }
}