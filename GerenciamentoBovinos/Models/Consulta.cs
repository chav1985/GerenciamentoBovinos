using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentoBovinos.Models
{
    public class Consulta
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("Bovino")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public long BovinoId { get; set; }
        public Bovino Bovino { get; set; }

        [ForeignKey("Veterinario")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public long VeterinarioId { get; set; }
        public Veterinario Veterinario { get; set; }

        [MaxLength(200, ErrorMessage = "A {0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Data da Consulta")]
        public DateTime DtServico { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Valor Total")]
        public decimal Valor { get; set; }
    }
}