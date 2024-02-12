using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TestWebAPI.Datos;
using TestWebAPI.Modelos;
using TestWebAPI.Modelos.Dto;

namespace TestWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db;
        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            _logger.LogInformation("Recuperando Villas");
            //return new List<VillaDto>()
            //{
            //    new VillaDto {Id = 1, Nombre="Otro"},
            //    new VillaDto {Id = 2, Nombre="Otro2"}
            //};

            // return Ok(VillaStore.villaList);

            return Ok(_db.Villas.ToList());
        }

        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al recibir el parametro");
                return BadRequest();
            }

            // var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            var villa = _db.Villas.FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
                _logger.LogWarning("Error al recibir el parametro");
                return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villa)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (_db.Villas.FirstOrDefault(v => v.Nombre.ToLower() == villa.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NameExist", "Ya existe la villa");
                return BadRequest(ModelState);
            }

            if (villa == null) return BadRequest(villa);

            if (villa.Id > 0) return StatusCode(StatusCodes.Status500InternalServerError);

            Villa newModel = new Villa()
            {
                Nombre = villa.Nombre,
                Detalle = villa.Nombre,
                ImagenURL = villa.ImagenURL,
                Ocupantes = villa.Ocupantes,
                Tarifa = villa.Tarifa,
                MetrosCuadrados = villa.MetrosCuadrados,
                Amenidad = villa.Amenidad,
            };

            _db.Villas.Add(newModel);
            _db.SaveChanges();

            // return Ok(villa);
            return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0) return BadRequest();

            var villa = _db.Villas.FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            _db.Villas.Remove(villa);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaNew)
        {
            if (villaNew == null || villaNew.Id != id) return BadRequest(villaNew);

            var villaOld = _db.Villas.FirstOrDefault(x => x.Id == id);
            if (villaOld == null) return NotFound();

            Villa newModel = new Villa()
            {
                Id = id,
                Nombre = villaNew.Nombre,
                Detalle = villaNew.Nombre,
                ImagenURL = villaNew.ImagenURL,
                Ocupantes = villaNew.Ocupantes,
                Tarifa = villaNew.Tarifa,
                MetrosCuadrados = villaNew.MetrosCuadrados,
                Amenidad = villaNew.Amenidad,
            };

            _db.Villas.Update(newModel);
            _db.SaveChanges();

            //villaOld.Ocupantes = villaNew.Ocupantes;
            //villaOld.MetrosCuadrados = villaNew.MetrosCuadrados;

            // VillaStore.villaList.inde

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> villaNew)
        {
            if (villaNew == null || id == 0) return BadRequest(villaNew);

            // var villaOld = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            // if (villaOld == null) return NotFound();

            // villaNew.ApplyTo(villaOld, ModelState);
            var villaOld = _db.Villas.FirstOrDefault(x => x.Id == id);

            VillaDto villaDto = new VillaDto() {
                Id = id,
                Nombre = villaOld.Nombre,
                ImagenURL = villaOld.ImagenURL,
                Ocupantes = villaOld.Ocupantes,
                Tarifa = villaOld.Tarifa,
                MetrosCuadrados = villaOld.MetrosCuadrados,
                Amenidad = villaOld.Amenidad,
            };

            if(villaOld == null || id == 0) { return BadRequest(villaNew); }

            villaNew.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid) return BadRequest();
            
            Villa newModel = new Villa()
            {
                Id = id,
                Nombre = villaOld.Nombre,
                Detalle = villaOld.Nombre,
                ImagenURL = villaOld.ImagenURL,
                Ocupantes = villaOld.Ocupantes,
                Tarifa = villaOld.Tarifa,
                MetrosCuadrados = villaOld.MetrosCuadrados,
                Amenidad = villaOld.Amenidad,
            };

            _db.Villas.Update(newModel);
            _db.SaveChanges();

            return NoContent();
        }

    }
}
