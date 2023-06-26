using Microsoft.AspNetCore.Mvc;
using tiendaAPI.Datos;
using tiendaAPI.modelo;

namespace tiendaAPI.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class productosController:ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Mproducts>>> Get()
        {
            var funcion = new Dproductos();
            var lista = await funcion.MostrarProductos();
            return lista;
        }

        [HttpPost]
        public async Task Post([FromBody] Mproducts parametros)
        {
            var funcion = new Dproductos();
            await funcion.inserProducts(parametros);
        }

        [HttpPut ("{id}")]
        public async Task <ActionResult> Put(int id, [FromBody] Mproducts parametros)
        {
            var funcion = new Dproductos();
            parametros.id = id;
            await funcion.editarProducts(parametros);
            return NoContent();
        }
    }
}
