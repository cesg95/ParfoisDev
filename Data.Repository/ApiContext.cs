namespace Data.Repository
{
    using Domain.Model;

    using Microsoft.EntityFrameworkCore;

    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ParfoisDev");
        }

        public DbSet<Pedido> Pedidos { get; set; }
    }
}
