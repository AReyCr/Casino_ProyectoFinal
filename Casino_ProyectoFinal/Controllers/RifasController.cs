using AutoMapper;
using Casino_ProyectoFinal.DTOs;
using Casino_ProyectoFinal.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Casino_ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/Rifas")]
    public class RifasController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<RifasController> logger;
        private readonly IMapper mapper;

        public RifasController(ApplicationDbContext context, ILogger<RifasController> logger, IMapper mapper)
        {
            this.logger = logger;
            this.dbContext = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]RifasDTO rifasDto)
        {
            var exist = await dbContext.Rifas.AnyAsync(x => x.Nombre == rifasDto.Nombre);

            if (exist)
            {
                return BadRequest("Ya existe rifa con ese nombre");
            }

            var rifa = mapper.Map<Rifas>(rifasDto);

            dbContext.Add(rifa);
            await dbContext.SaveChangesAsync();

            var rifasDTO = mapper.Map<GetRifasDTO>(rifa);

            return CreatedAtRoute("ObtonerRifa", new {id=rifa.Id}, rifasDTO);
        }

        [HttpGet]
        public async Task<ActionResult<List<GetRifasDTO>>> GetAll()
        {
            var rifas = await dbContext.Rifas.ToListAsync();
            return mapper.Map<List<GetRifasDTO>>(rifas);
        }

        [HttpGet("{id:int}", Name = "ObtenerRifa")]
        public async Task<ActionResult<RifasDTO>> GetById(int id)
        {
            var rifa = await dbContext.Rifas.FirstOrDefaultAsync(x => x.Id == id);
            if( rifa == null)
            { 
                return NotFound();
            }
           
            return mapper.Map<RifasDTO>(rifa);
        }
        /*
        [HttpGet("NumerosDisponibles /{id]")]    // Metodo pendiente oara mostrar el listado de numeros disponibles de la r
        public async Task<ActionResult<Rifas>> Get(int id)
        {
            return await dbContext.Rifas.FirstOrDefaultAsync(x => x.Id == id);
        }

        */

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(RifasDTO rifasDTO, int id)
        {
            var exist = await dbContext.Rifas.AnyAsync(x=> x.Id == id);
            var exist2 = await dbContext.Rifas.AnyAsync(x => x.Nombre == rifasDTO.Nombre);

            if (!exist)
            {
                return BadRequest("Rifa no existente");
            }

            if (!exist2)
            {
                return BadRequest("Ya existe rifa con ese nombre");
            }

            var rifa = mapper.Map<Rifas>(rifasDTO);

            rifa.Id = id;

            dbContext.Update(rifa);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Rifas.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }
            dbContext.Remove(new Rifas()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
