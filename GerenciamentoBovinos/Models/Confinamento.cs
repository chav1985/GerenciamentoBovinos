using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentoBovinos.Models
{
    public class Confinamento
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("Bovino")]
        public long BovinoId { get; set; }
        public Bovino Bovino { get; set; }

        [Display(Name = "Data Entrada")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DtEntrada { get; set; }

        [Display(Name = "Data Saída")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DtSaida { get; set; }

        [Display(Name = "Custo total")]
        public decimal CustoTotal { get; set; }

        public List<BaixaProduto> LsBaixaProdutos { get; set; }

        public List<Consulta> LsConsultas { get; set; }
    }
}