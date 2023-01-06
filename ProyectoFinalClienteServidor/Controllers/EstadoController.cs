using Microsoft.AspNetCore.Mvc;
using ProyectoFinalClienteServidor.DTOS;
using ProyectoFinalClienteServidor.Models;
using ProyectoFinalClienteServidor.Repositories;

namespace ProyectoFinalClienteServidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly sistem21_aeromexdbContext context;
        Repository<Estado> repositoryestado;
        public EstadoController(sistem21_aeromexdbContext cx)
        {
            this.context = cx;
            repositoryestado = new(context);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var data = repositoryestado.Get().OrderBy(x => x.Estado1);

            return Ok(data.Select(x => new EstadoDTO
            {
                Estado1 = x.Estado1,
                Id = x.IdEstado
            }));
        }
        [HttpPost]
        public IActionResult Post(EstadoDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Estado1))
                return BadRequest("Debe especificar el estado que desee agregar.");
            if (repositoryestado.Get().Any(x => x.Estado1 == dto.Estado1 && x.IdEstado != dto.Id))
                return BadRequest("Ya existe un estado identico al que deseas registrar.");
            Estado entidad = new()
            {
                Estado1 = dto.Estado1
            };
            repositoryestado.Insert(entidad);
            return Ok();
        }
        [HttpPut]
        public IActionResult Put(EstadoDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Estado1))
                return BadRequest("Debe especificar el estado que desee agregar.");
            if (repositoryestado.Get().Any(x => x.Estado1 == dto.Estado1 && x.IdEstado != dto.Id))
                return BadRequest("Ya existe un estado identico al que deseas registrar.");
            var entidad = repositoryestado.Get(dto.Id);
            if (entidad == null)
                return NotFound();
            entidad.Estado1 = dto.Estado1;
            repositoryestado.Update(entidad);
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete(EstadoDTO dto)
        {
            if (dto == null)
                return BadRequest("Especifica el estado a eliminar.");
            Repository<Vuelo> prueba = new Repository<Vuelo>(context);
            if (prueba.Get().Any(x => x.IdEstado == dto.Id))
                return Conflict("El estado no puede ser eliminado debido a que existen vuelos con ese estado.");
            var entidad = repositoryestado.Get(dto.Id);
            if (entidad == null)
                return NotFound();
            repositoryestado.Delete(entidad);
            return Ok();
        }
    }
}
