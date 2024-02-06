using System.ComponentModel.DataAnnotations;

namespace TestWebAPI.Modelos.Dto
{
    public class VillaDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public required string Nombre { get; set; }

        public int Ocupantes { get; set; }

        public int MetrosCuadrados { get; set; }

    }
}
