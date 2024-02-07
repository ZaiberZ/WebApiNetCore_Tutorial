using Microsoft.EntityFrameworkCore;
using TestWebAPI.Modelos;

namespace TestWebAPI.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) 
        {
            
        }
        public DbSet<Villa> Villas { get; set; }
    }
}
