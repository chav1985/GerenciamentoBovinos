using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GerenciamentoBovinos.Models
{
    public class Acesso
    {
        [Key]
        public long Id_login { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [MaxLength(100, ErrorMessage = "A {0} deve ter no máximo {1} caracteres")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Senha { get; set; }

        [MaxLength(1, ErrorMessage = "A {0} deve ter no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Ativo { get; set; }

        [MaxLength(15, ErrorMessage = "A {0} deve ter no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Perfil { get; set; }

        [MaxLength(30, ErrorMessage = "A {0} deve ter no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }

        [MaxLength(30, ErrorMessage = "A {0} deve ter no máximo {1} caracteres")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Sobrenome { get; set; }
    }
}