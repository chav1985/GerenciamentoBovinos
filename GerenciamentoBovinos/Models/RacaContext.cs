using System.Data.Entity;

namespace GerenciamentoBovinos.Models
{
    public class RacaContext : DbContext
    {
        public DbSet<Raca> Racas { get; set; }
    }
}