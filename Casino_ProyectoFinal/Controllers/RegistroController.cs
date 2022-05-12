using Casino_ProyectoFinal.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Casino_ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/Registro")]
    public class RegistroController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public RegistroController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Registro>>> Get()
        {
            return await dbContext.Registro.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Registro registro)
        {
            dbContext.Add(registro);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
/*
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Registro registro, int id)
        {
            if (registro.Id != id) 
            {
                return BadRequest("El id del participante no coincide con el establecido en la url");
            }

            dbContext.Update(registro);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Registro.AnyAsync(x=> x.Id==id);
            if (!exist)
            {
                return NotFound();
            }
            dbContext.Remove(new Registro()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
*/
    }

}
