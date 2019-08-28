using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GerenciamentoBovinos.Models
{
    public class GerenciamentoContext : DbContext
    {
        public GerenciamentoContext() : base("name=DbTeste5")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Raca> Racas { get; set; }

        public virtual DbSet<Bovino> Bovinos { get; set; }

        public virtual DbSet<TipoProduto> TipoProdutos { get; set; }

        public virtual DbSet<Produto> Produtos { get; set; }

        public virtual DbSet<Veterinario> Veterinarios { get; set; }
    }
}