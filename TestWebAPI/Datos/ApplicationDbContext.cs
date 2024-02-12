using Microsoft.EntityFrameworkCore;
using TestWebAPI.Modelos;

namespace TestWebAPI.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Nombre = "Aldea",
                    Detalle = "Rustica",
                    ImagenURL = "",
                    Ocupantes = 5,
                    MetrosCuadrados = 50,
                    Tarifa = 200,
                    Amenidad = "",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                },
                new Villa()
                {
                    Id = 2,
                    Nombre = "Pueblo",
                    Detalle = "Alto",
                    ImagenURL = "",
                    Ocupantes = 1,
                    MetrosCuadrados = 500,
                    Tarifa = 20,
                    Amenidad = "",
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
            //base.OnModelCreating(modelBuilder);
        }
    }
}
