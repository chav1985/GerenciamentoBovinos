using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GerenciamentoBovinos.Models
{
    public class Endereco
    {
        [MaxLength(60, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string Rua { get; set; }

        [MaxLength(20, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string Numero { get; set; }

        [MaxLength(30, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string Estado { get; set; }

        [MaxLength(30, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string Cidade { get; set; }

        [MaxLength(9, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string Cep { get; set; }

        [MaxLength(80, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string Complemento { get; set; }
    }
}