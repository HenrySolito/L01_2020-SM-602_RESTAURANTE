using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Options;

namespace L01_2020_SM602.Models
{
    public class restauranteContext : DbContext
    {
        public restauranteContext(DbContextOptions<restauranteContext> options) : base(options)
        {

        }

        public DbSet<platos> platos { get; set; }
        public DbSet<clientes> clientes { get; set; }
        public DbSet<pedidos> pedidos { get; set;}
        public DbSet<motoristas> motoristas { get; set;}
    }
}
