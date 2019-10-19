using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GerenciamentoBovinos.Models
{
    public class BaixaProduto
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Data de Utilização")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DtUtilizacao { get; set; }

        public virtual Bovino Bovino { get; set; }
        [ForeignKey("Bovino")]
        public long BovinoId { get; set; }

        [Display(Name = "Valor Total")]
        public decimal ValorTotal { get; set; }

        public virtual List<ItemsBaixaProduto> Items { get; set; }
    }
}