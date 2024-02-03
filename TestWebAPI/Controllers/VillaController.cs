using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestWebAPI.Datos;
using TestWebAPI.Modelos.Dto;

namespace TestWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<VillaDto> GetVillas()
        {
            //return new List<VillaDto>()
            //{
            //    new VillaDto {Id = 1, Nombre="Otro"},
            //    new VillaDto {Id = 2, Nombre="Otro2"}
            //};
            return VillaStore.villaList;
        }

        [HttpGet("id:int")]
        public VillaDto GetVilla(int id)
        {
            return VillaStore.villaList.FirstOrDefault(x => x.Id == id);
        }
    }
}
