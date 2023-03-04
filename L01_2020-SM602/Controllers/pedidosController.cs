﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020_SM602.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020_SM602.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pedidosController : ControllerBase
    {
        private readonly restauranteContext _restauranteContext;

        public pedidosController(restauranteContext clientesContext) 
        {
            _restauranteContext= clientesContext;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult nuevoPedido([FromBody] pedidos Npedido)
        {
            try
            {
                _restauranteContext.pedidos.Add(Npedido);
                _restauranteContext.SaveChanges();
                return Ok(Npedido);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarPedido(int id, [FromBody] pedidos pedidoModificar)
        {
            pedidos? pedidoActual =(from e in _restauranteContext.pedidos
                                    where e.pedidoId== id
                                    select e).FirstOrDefault();

            if (pedidoActual == null)
            {
                return NotFound();
            }
            //Modificacion de los campos si encuentra el registro
            pedidoActual.pedidoId= pedidoModificar.pedidoId;
            pedidoActual.motoristaId = pedidoModificar.motoristaId;
            pedidoActual.clienteId = pedidoModificar.motoristaId;
            pedidoActual.platoId = pedidoModificar.platoId;
            pedidoActual.cantidad = pedidoModificar.cantidad;
            pedidoActual.precio = pedidoModificar.precio;

            _restauranteContext.Entry(pedidoActual).State = EntityState.Modified;
            _restauranteContext.SaveChanges();

            return Ok(pedidoModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarPedido(int id)
        {
            pedidos? pedido = (from e in _restauranteContext.pedidos
                               where e.pedidoId== id
                               select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }

            _restauranteContext.pedidos.Attach(pedido);
            _restauranteContext.pedidos.Remove(pedido);
            _restauranteContext.SaveChanges();

            return Ok(pedido);
        }

        [HttpGet]
        [Route("GetbyIdCliente/{id}")]
        public IActionResult GetPedidoCliente(int id)
        {
            pedidos? pedido = (from e in _restauranteContext.pedidos
                               where e.pedidoId== id
                               select e).FirstOrDefault();

            if(pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        [HttpGet]
        [Route("GetbyIdMotorista/{id}")]
        public IActionResult GetPedidoMotorista(int id)
        {
            pedidos? pedido = (from e in _restauranteContext.pedidos
                               where e.motoristaId == id
                               select e).FirstOrDefault();

            if(pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }
    }
}
