using System.ComponentModel.DataAnnotations;

namespace TestWebAPI.Modelos.Dto
{
    public class VillaUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public required string Nombre { get; set; }

        [Required]
        public double Tarifa { get; set; }

        [Required]
        public int Ocupantes { get; set; }
        [Required]
        public int MetrosCuadrados { get; set; }
        [Required]
        public string ImagenURL { get; set; }
        public string Amenidad { get; set; }

    }
}
