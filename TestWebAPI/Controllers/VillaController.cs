using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            //return new List<VillaDto>()
            //{
            //    new VillaDto {Id = 1, Nombre="Otro"},
            //    new VillaDto {Id = 2, Nombre="Otro2"}
            //};
            return Ok(VillaStore.villaList);
        }

        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
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

            if (VillaStore.villaList.FirstOrDefault(v => v.Nombre.ToLower() == villa.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NameExist", "Ya existe la villa");
                return BadRequest(ModelState);
            }

            if (villa == null) return BadRequest(villa);

            if (villa.Id > 0) return StatusCode(StatusCodes.Status500InternalServerError);

            villa.Id = VillaStore.villaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villa);

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

            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            VillaStore.villaList.Remove(villa);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaNew)
        {
            if (villaNew == null || villaNew.Id != id) return BadRequest(villaNew);

            var villaOld = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            if (villaOld == null) return NotFound();

            villaOld.Ocupantes = villaNew.Ocupantes;
            villaOld.MetrosCuadrados = villaNew.MetrosCuadrados;

            // VillaStore.villaList.inde

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> villaNew)
        {
            if (villaNew == null || id == 0) return BadRequest(villaNew);

            var villaOld = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            if (villaOld == null) return NotFound();

            villaNew.ApplyTo(villaOld, ModelState);

            if (!ModelState.IsValid) return BadRequest();


            return NoContent();
        }

    }
}
