using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentoBovinos.Models
{
    public class Bovino
    {
        [Key]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public long Brinco { get; set; }

        [ForeignKey("Raca")]
        public long RacaId { get; set; }
        public Raca Raca { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public decimal Peso { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MinLength(3, ErrorMessage = "O {0} deve ter no mínimo {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string Lote { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Data Nascimento")]
        public DateTime DtNascimento { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Vlr Unitário")]
        public decimal VlrUnitario { get; set; }
        [DataType(DataType.MultilineText)]
        [MaxLength(200, ErrorMessage = "A {0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}