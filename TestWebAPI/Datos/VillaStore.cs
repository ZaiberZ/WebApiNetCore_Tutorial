using TestWebAPI.Modelos.Dto;

namespace TestWebAPI.Datos
{
    public class VillaStore
    {
        public static List<VillaDto> villaList =  new List<VillaDto> { 
            new VillaDto() { Id = 1,  Nombre = "Villa 1"},
            new VillaDto() { Id = 2,  Nombre = "Villa 2"}
        };
    }
}
