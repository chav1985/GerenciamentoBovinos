using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GerenciamentoBovinos.Models
{
    public class GerenciamentoContext : DbContext
    {
        public GerenciamentoContext() : base("name=DbTeste17")
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

        public virtual DbSet<Fornecedor> Fornecedores { get; set; }

        public virtual DbSet<Cliente> Clientes { get; set; }

        public virtual DbSet<Fretes> Fretes { get; set; }

        public virtual DbSet<Comissionado> Comissionados { get; set; }

        public virtual DbSet<Motorista> Motoristas { get; set; }

        public virtual DbSet<Consulta> Consultas { get; set; }

        public virtual DbSet<CompraProdutos> CompraProdutos { get; set; }
    }
}