namespace Data.Repository
{
    using Domain.Model;

    using Microsoft.EntityFrameworkCore;

    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ParfoisDev");
        }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
