using System;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoBovinos.Models
{
    public class Fretes
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Date de Inicio")]
        public DateTime DateInicio { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Date de Chegada")]
        public DateTime DateChegada { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Preço")]
        public decimal  Preco { get; set; }

        [MaxLength(200, ErrorMessage = "A {0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Peso de Saída")]
        public int PesoSaida { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Peso de Chegada")]
        public int PesoChegada { get; set; }

    }
}