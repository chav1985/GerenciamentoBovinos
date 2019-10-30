using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace GerenciamentoBovinos.Models
{
    public class GerenciamentoContext : DbContext
    {
        public GerenciamentoContext() : base("name=DbBoi010")
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

        public virtual DbSet<Consulta> Consultas { get; set; }

        public virtual DbSet<VendaProduto> VendaProdutos { get; set; }

        public virtual DbSet<ItemsVendaProduto> ItemsVendaProdutos { get; set; }

        public virtual DbSet<Confinamento> Confinamentos { get; set; }

        public virtual DbSet<BaixaProduto> BaixaProdutos { get; set; }

        public virtual DbSet<ItemsBaixaProduto> ItemsBaixaProdutos { get; set; }

        public virtual DbSet<VendaBovino> VendaBovinos { get; set; }
    }
}