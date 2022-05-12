using Casino_ProyectoFinal.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Casino_ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/Participantes")]
    public class ParticipantesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ParticipantesController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Participantes participantes)
        {
            dbContext.Add(participantes);
            await dbContext.SaveChangesAsync();
            return Ok();
        
        }

        [HttpGet]    // Get para datos relacionados ( RIFAS )
        public async Task<ActionResult<List<Participantes>>> Get()
        {
            return await dbContext.Participantes.Include(x => x.Rifas).ToListAsync();
        }

        /*
        [HttpGet]    // Get para datos relacionados ( REGISTROS )
        public async Task<ActionResult<List<Participantes>>> Get() 
        {
            return await dbContext.Participantes.Include(x=> x.Registro).ToListAsync();
        }

        
        [HttpGet]
        public async Task<ActionResult<List<Participantes>>> Get()     Get para obtencion de datos sin relacion 
        {
            return await dbContext.Participantes.ToListAsync();
        }
        */

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Participantes participantes, int id)
        {
            if (participantes.Id != id)
            {
                return BadRequest("El id del participante no coincide con el establecido en la url");
            }

            dbContext.Update(participantes);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
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