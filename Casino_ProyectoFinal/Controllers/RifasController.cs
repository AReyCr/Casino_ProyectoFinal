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

        public RifasController(ApplicationDbContext context)
        {
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Rifas>> Get(int id)
        {
            return await dbContext.Rifas.FirstOrDefaultAsync(x=>x.Id==id);
        }

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
