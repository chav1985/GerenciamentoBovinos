using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentoBovinos.Models
{
    public class Consulta
    {
        [Key]
        public long Id { get; set; }

        public virtual Bovino Bovino { get; set; }
        [ForeignKey("Bovino")]
        public long BovinoId { get; set; }

        public virtual Veterinario Veterinario { get; set; }
        [ForeignKey("Veterinario")]
        public long VeterinarioId { get; set; }

        [MaxLength(200, ErrorMessage = "A {0} deve ter no máximo {1} caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Data da Consulta")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DtServico { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Valor Total")]
        public decimal Valor { get; set; }
    }
}