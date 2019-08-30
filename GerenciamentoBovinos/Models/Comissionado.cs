using System.ComponentModel.DataAnnotations;

namespace GerenciamentoBovinos.Models
{
    public class Comissionado : Pessoa
    {
        [Key]

        public long Id { get; set; }

    }
}