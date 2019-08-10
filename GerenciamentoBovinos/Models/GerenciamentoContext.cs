using System.Data.Entity;

namespace GerenciamentoBovinos.Models
{
    public class GerenciamentoContext : DbContext
    {
        public DbSet<Raca> Racas { get; set; }
    }
}