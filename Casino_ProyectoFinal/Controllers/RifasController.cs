using AutoMapper;
using Casino_ProyectoFinal.DTOs;
using Casino_ProyectoFinal.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Casino_ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/Rifas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
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
        public async Task<ActionResult> Post([FromBody] RifasDTO rifasDto)
        {
            var exist = await dbContext.Rifas.AnyAsync(x => x.Nombre == rifasDto.Nombre);
            //  var max = await dbContext.Rifas.MaxAsync(y => y.Id = id);
            if (exist)
            {
                return BadRequest("Ya existe rifa con ese nombre");
            }

            var rifa = mapper.Map<Rifas>(rifasDto);

            dbContext.Add(rifa);
            await dbContext.SaveChangesAsync();

            var rifasDTO = mapper.Map<GetRifasDTO>(rifa);

            return CreatedAtRoute("ObtonerRifa", new { id = rifa.Id }, rifasDTO);
        }

        [HttpGet]
        public async Task<ActionResult<List<GetRifasDTO>>> GetAll()
        {
            var rifas = await dbContext.Rifas.ToListAsync();
            return mapper.Map<List<GetRifasDTO>>(rifas);
        }
        /*
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
        */
        [HttpGet("Ganador" )] // GUARDAR TARJETA
        public async Task<ActionResult<ParticipantesDTO>> Get(int id)
        {
            var rifa = await dbContext.Rifas.FirstOrDefaultAsync(x => x.Id == id);
            Random rand = new Random();
            int toSkip = rand.Next(1,dbContext.Participantes.Count());
            dbContext.Participantes.Skip(toSkip).Take(1).First();
            var tarjetasDto = await dbContext.Participantes.OrderBy(y => Guid.NewGuid()).Skip(toSkip).Take(1).FirstOrDefaultAsync();
            if (id == tarjetasDto.RifasId)
            {
                return  mapper.Map<ParticipantesDTO>(tarjetasDto);
            }
           
            if (rifa == null)
            {
                return NotFound();
            }
            /*
            var participante = mapper.Map<TarjetasDTO>(tarjetasDto);
            dbContext.Add(tarjetasDto);
            await dbContext.SaveChangesAsync();
            */

            return Ok();
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
           // var exist2 = await dbContext.Rifas.AnyAsync(y => y.Nombre == rifasDTO.Nombre);

            if (!exist)
            {
                return BadRequest("Rifa no existente");
            }

           /* if (!exist2)
            {
                return BadRequest("Ya existe rifa con ese nombre");
            }
           */
            var rifa = mapper.Map<Rifas>(rifasDTO);

            rifa.Id = id;

            dbContext.Update(rifa);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<ParticipantesPatchDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var participantesDB = await dbContext.Participantes.FirstOrDefaultAsync(x => x.Id == id);

            if (participantesDB == null)
            {
                return NotFound();
            }

            var participantesDTO = mapper.Map<ParticipantesPatchDTO>(participantesDB);

            patchDocument.ApplyTo(participantesDTO);

            var isValid = TryValidateModel(participantesDTO);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(participantesDTO, participantesDB);

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

        [HttpDelete("ELiminarParticipante")]
        public async Task<ActionResult> DeletebyId(int id)
        {
            var exist = await dbContext.Participantes.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El recurso no fue encontrado");
            }

            dbContext.Remove(new Participantes { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
