using L01_2020_SM602.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;

namespace L01_2020_SM602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class clientesController : ControllerBase
    {
        private readonly restauranteContext _restauranteContext;

        public clientesController(restauranteContext restauranteContext)
        {
            _restauranteContext = restauranteContext;
        }

        //Consultar Clientes
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetClientes() 
        {
            List<clientes> listadoClientes = (from e in _restauranteContext.clientes
                                              select e).ToList();
            if(listadoClientes.Count == 0)return NotFound();

            return Ok(listadoClientes);
        }

        //Agregar nuevo Cliente
        [HttpPost]
        [Route("Add")]
        public IActionResult nuevoCliente([FromBody] clientes Ncliente)
        {
            try
            {
                _restauranteContext.clientes.Add(Ncliente);
                _restauranteContext.SaveChanges();
                return Ok(Ncliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Actualizar/Modificar Cliente
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarCliente(int id, [FromBody] clientes clienteModificar)
        {
            clientes? clienteActual = (from e in _restauranteContext.clientes where
                                       e.clienteId == id
                                       select e).FirstOrDefault();

            if(clienteActual == null) return NotFound();

            //Modificacion de los campos si encuentra el registro
            clienteActual.nombreCliente = clienteModificar.nombreCliente;
            clienteActual.direccion = clienteModificar.direccion;

            _restauranteContext.Entry(clienteActual).State = EntityState.Modified;
            _restauranteContext.SaveChanges();

            return Ok(clienteModificar);
        }

        //Eliminar Plato
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarCliente(int id)
        {
            clientes? cliente = (from e in _restauranteContext.clientes
                                 where e.clienteId == id
                                 select e).FirstOrDefault();

            if(cliente == null) return NotFound();

            _restauranteContext.clientes.Attach(cliente);
            _restauranteContext.clientes.Remove(cliente);
            _restauranteContext.SaveChanges();

            return Ok(cliente);
        }

        [HttpGet]
        [Route("Find/{filtro}")]
        public IActionResult FindByClientDir(string filtro) 
        {
            clientes? DirC = (from e in _restauranteContext.clientes
                              where e.direccion.Contains(filtro)
                              select e).FirstOrDefault();

            if (DirC == null) return NotFound();

            return Ok(DirC);
        }
    }
}
