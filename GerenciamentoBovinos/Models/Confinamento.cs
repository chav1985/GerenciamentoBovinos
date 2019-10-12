using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentoBovinos.Models
{
    public class Confinamento
    {
        public Confinamento()
        {
            DtEntrada = DateTime.Today;
        }

        [Key]
        public long Id { get; set; }

        [ForeignKey("Bovino")]
        public long BovinoId { get; set; }
        public Bovino Bovino { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Display(Name = "Data Entrada")]
        public DateTime DtEntrada { get; set; }

        [Display(Name = "Data Saída")]
        public DateTime? DtSaida { get; set; }

        [Display(Name = "Custo total")]
        public decimal CustoTotal { get; set; }

        public List<VendaProduto> LsVendaProdutos { get; set; }

        public List<Consulta> LsConsultas { get; set; }
    }
}