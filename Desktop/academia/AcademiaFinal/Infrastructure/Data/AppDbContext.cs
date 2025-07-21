using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NexusGym.Domain;

namespace NexusGym.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Equipamento> Equipamentos { get; set; } 
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Plano> Planos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ItemFichaDeTreino> ItensFichaDeTreino { get; set; }
        public DbSet<PlanoCliente> PlanoClientes { get; set; }
        public DbSet<FichaDeTreino> FichasDeTreino { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options) { }

    }

}
