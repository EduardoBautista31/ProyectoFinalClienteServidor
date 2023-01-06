using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalClienteServidor.DTOS;
using ProyectoFinalClienteServidor.Models;
using ProyectoFinalClienteServidor.Repositories;

namespace ProyectoFinalClienteServidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VueloController : ControllerBase
    {
        private readonly sistem21_aeromexdbContext context;
        Repository<Vuelo> repositoryVuelo;
        Repository<Estado> repositoryEstado;
        public VueloController(sistem21_aeromexdbContext cx)
        {
            this.context = cx;
            repositoryVuelo = new(context);
            repositoryEstado = new(context);
        }
        [HttpPost]
        public IActionResult Post(VueloDTO p)
        {
            if (p == null)
            {
                return BadRequest("Debe proporcionar un producto");
            }

            if (Validate(p, out List<string> errores))
            {
                Vuelo vuelo = new()
                {
                    CodigoVuelo = p.CodigoVuelo,
                    Salida = p.Salida,
                    IdEstado = repositoryEstado.Get().Where(x => x.Estado1 == p.Estado).Select(x => x.IdEstado).FirstOrDefault(),
                    Hora =TimeOnly.FromTimeSpan(p.Hora),
                    HoraLlegada = TimeOnly.FromTimeSpan(p.HoraLlegada)
                };
                repositoryVuelo.Insert(vuelo);
                return Ok();
            }
            else
            {
                return BadRequest(errores);
            }
        }

        private bool Validate(VueloDTO dto, out List<string> errores)
        {
            errores = new List<string>();
            if (string.IsNullOrWhiteSpace(dto.CodigoVuelo))
                errores.Add("Escriba el codigo de vuelo.");
            if (string.IsNullOrWhiteSpace(dto.Salida))
                errores.Add("Escriba la salida, la salida es el lugar de donde viene el vuelo.");
            if (repositoryVuelo.Get().Any(x => x.CodigoVuelo == dto.CodigoVuelo && x.IdVuelo != dto.IdVuelo))
                errores.Add("Ese codigo de vuelo ya se encuentra en vuelo.");
            return errores.Count == 0;
        }

        [HttpGet("get")]
        public IActionResult Get()
        {
            var vuelos = repositoryVuelo.Get()
                .Include(x => x.IdEstadoNavigation).OrderBy(x => x.HoraLlegada)
                .Select(x => new VueloDTO
                {
                    CodigoVuelo = x.CodigoVuelo,
                    Salida = x.Salida,
                    Estado = x.IdEstadoNavigation.Estado1,
                    Hora = x.Hora.ToTimeSpan(),
                    HoraLlegada = x.HoraLlegada.ToTimeSpan(),
                    IdVuelo = x.IdVuelo
                });
            return Ok(vuelos);
        }
       
        [HttpPost("put")]
        public IActionResult Put(VueloDTO dto)
        {
            if (dto == null)
                return BadRequest("Favor de seleccionar el vuelo a editar");
            if (Validate(dto, out List<string> errores))
            {
                var entidad = repositoryVuelo.Get(dto.IdVuelo);
                if (entidad == null)
                    return NotFound();
                entidad.CodigoVuelo = dto.CodigoVuelo;
                entidad.Salida = dto.Salida;
                entidad.IdEstado = repositoryEstado.Get()
                    .Where(x => x.Estado1 == dto.Estado)
                    .Select(x => x.IdEstado).FirstOrDefault();
                entidad.Hora = TimeOnly.FromTimeSpan(dto.Hora);
                entidad.HoraLlegada = TimeOnly.FromTimeSpan( dto.HoraLlegada);
                repositoryVuelo.Update(entidad);
                return Ok();
            }
            else
                return BadRequest(errores);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entidad = repositoryVuelo.Get(id);
            if (entidad == null)
                return NotFound();
            repositoryVuelo.Delete(entidad);
            return Ok();
        }
        [HttpGet("get/eliminar")]
        public IActionResult GetE()
        {
            var vuelos = repositoryVuelo.Get()
                .Include(x => x.IdEstadoNavigation).Where(x=>x.IdEstadoNavigation.Estado1=="Cancelado" || 
                x.IdEstadoNavigation.Estado1 == "Arrivando")
                .OrderBy(x => x.HoraLlegada)
                .Select(x => new VueloDTO
                {
                    CodigoVuelo = x.CodigoVuelo,
                    Salida = x.Salida,
                    Estado = x.IdEstadoNavigation.Estado1,
                    Hora = x.Hora.ToTimeSpan(),
                    HoraLlegada = x.HoraLlegada.ToTimeSpan(),
                    IdVuelo = x.IdVuelo
                });
            return Ok(vuelos);
        }
    }
   
}
