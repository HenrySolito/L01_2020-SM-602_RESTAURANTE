using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020_SM602.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020_SM602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class platosController : ControllerBase
    {
        private readonly restauranteContext _restauranteContext;

        public platosController(restauranteContext restauranteContext)
        {
            _restauranteContext= restauranteContext;
        }

        //Consultar Platos
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetPlatos() 
        {
            List<platos> listadoPlatos = (from e in _restauranteContext.platos
                                          select e).ToList();

            if(listadoPlatos.Count == 0)return NotFound();

            return Ok(listadoPlatos);
        }


        //Agregar un nuevo plato
        [HttpPost]
        [Route("Add")]
        public IActionResult nuevoPlato([FromBody] platos Nplato)
        {
            try
            {
                _restauranteContext.platos.Add(Nplato);
                _restauranteContext.SaveChanges();
                return Ok(Nplato);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Actualizar/Modifica plato
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarPlato(int id, [FromBody] platos platoModificar)
        {
            platos? platoActual = (from e in _restauranteContext.platos
                                     where e.platoId == id
                                     select e).FirstOrDefault();

            if (platoActual == null)
            {
                return NotFound();
            }
            //Modificacion de los campos si encuentra el registro
            platoActual.nombrePlato = platoModificar.nombrePlato;
            platoActual.precio = platoModificar.precio;


            _restauranteContext.Entry(platoActual).State = EntityState.Modified;
            _restauranteContext.SaveChanges();

            return Ok(platoModificar);
        }

        //Eliminar Plato
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarPlato(int id)
        {
            platos? plato = (from e in _restauranteContext.platos
                               where e.platoId == id
                               select e).FirstOrDefault();

            if (plato == null)
            {
                return NotFound();
            }

            _restauranteContext.platos.Attach(plato);
            _restauranteContext.platos.Remove(plato);
            _restauranteContext.SaveChanges();

            return Ok(plato);
        }

        //Consultar plato por nombre
        [HttpGet]
        [Route("Find/{filtro}")]
        public IActionResult FindByPlateName(string filtro)
        {
            platos? namPlate = (from e in _restauranteContext.platos
                                where e.nombrePlato.Contains(filtro)
                                select e).FirstOrDefault();

            if (namPlate == null)
            {
                return NotFound();
            }

            return Ok(namPlate);
        }

    }
}
