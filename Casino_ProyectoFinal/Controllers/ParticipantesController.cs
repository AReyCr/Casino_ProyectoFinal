using Casino_ProyectoFinal.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Casino_ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/alumnos")]
    public class ParticipantesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Participantes>> Get()
        {
            return new List<Participantes>()
            {
                new Participantes() { Id = 1, Nombre = "Bic"}
            };
        }
    }
}
