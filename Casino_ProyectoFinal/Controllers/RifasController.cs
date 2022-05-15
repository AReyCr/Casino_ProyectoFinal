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

        public RifasController(ApplicationDbContext context, ILogger<RifasController> logger)
        {
            this.logger = logger;
            this.dbContext = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Rifas rifas)
        {
            dbContext.Add(rifas);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<Rifas>>> GetAll()
        {
            return await dbContext.Rifas.ToListAsync();
        }

        [HttpGet("Obtener/{id}")]
        public async Task<ActionResult<Rifas>> Get([FromRoute]int id)
        {
            var rifa = await dbContext.Rifas.FirstOrDefaultAsync(x => x.Id == id);
            if( rifa == null)
            { 
                return NotFound();
            }
           
            return rifa;
        }
        /*
        [HttpGet("NumerosDisponibles /{id]")]    // Metodo pendiente oara mostrar el listado de numeros disponibles de la r
        public async Task<ActionResult<Rifas>> Get(int id)
        {
            return await dbContext.Rifas.FirstOrDefaultAsync(x => x.Id == id);
        }

        */

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Rifas rifas, int id)
        {
            if (rifas.Id != id)
            {
                return BadRequest("Rifa no existente");
            }

            dbContext.Update(rifas);
            await dbContext.SaveChangesAsync();
            return Ok();
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
