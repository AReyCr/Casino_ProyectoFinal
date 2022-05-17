using AutoMapper;
using Casino_ProyectoFinal.DTOs;
using Casino_ProyectoFinal.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Casino_ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/Participantes")]
   // [Authorize] // AUTORIZAR TODOS LOS METODOS
    public class ParticipantesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<ParticipantesController> logger;
        private readonly IMapper mapper;

        public ParticipantesController(ApplicationDbContext context, ILogger<ParticipantesController> logger, IMapper mapper)
        {
            this.logger = logger;
            this.dbContext = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ParticipantesDTO participantesDto)
        {
            var repetido = await dbContext.Participantes.AnyAsync(x => x.NumeroSeleccion == participantesDto.NumeroSeleccion);
            var rifa = await dbContext.Participantes.AnyAsync(y => y.RifasId == participantesDto.RifasId);

            if (repetido && rifa)
            {
                return BadRequest("Numero ya selecionado");
            }

            var participante = mapper.Map<Participantes>(participantesDto);

            dbContext.Add(participante);
            await dbContext.SaveChangesAsync();

            var participantesDTO = mapper.Map<GetParticipantesDTO>(participante);

            return CreatedAtRoute("ObtenerParticipantes", new {id=participante.Id}, participantesDTO);
        
        }

        
        [HttpGet( Name="ObtenerParticipantes")]    // Get para datos relacionados ( RIFAS )
        
        public async Task<ActionResult<List<GetParticipantesDTO>>> Get()
        {
            logger.LogInformation("Se obtiene el listado de los participantes");
            logger.LogWarning("Mensaje de prueba warning");
            var participantes = await dbContext.Participantes.Include(x => x.Rifas).ToListAsync();
            return mapper.Map<List<GetParticipantesDTO>>(participantes);
        }



        /*
        [HttpGet]    // Get para datos relacionados ( REGISTROS )
        public async Task<ActionResult<List<Participantes>>> Get() 
        {
            return await dbContext.Participantes.Include(x=> x.Registro).ToListAsync();
        }
        
        
       [HttpGet( Name="ObtenerParticipantes")]
        public async Task<ActionResult<List<GetParticipantesDTO>>> Get()   //  Get para obtencion de datos sin relacion 
        {
            var participantes = await dbContext.Participantes.ToListAsync();
            return mapper.Map<List<GetParticipantesDTO>>(participantes);
   
        }
       */

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(ParticipantesDTO participantesDTO, int id)
        {

            var repetido = await dbContext.Participantes.AnyAsync(x => x.NumeroSeleccion == participantesDTO.NumeroSeleccion);
            var rifa = await dbContext.Participantes.AnyAsync(y => y.RifasId == participantesDTO.RifasId);
            var exist = await dbContext.Participantes.AnyAsync(z => z.Id == id);

            if (repetido && rifa)
            {
                return BadRequest("Numero ya selecionado");
            }

            if (!exist)
            {
                return BadRequest("El id del participante no coincide con el establecido en la url");
            }

            var participante = mapper.Map<Participantes>(participantesDTO);
            participante.Id = id;
            
            dbContext.Update(participante);
            await dbContext.SaveChangesAsync();
            return NoContent();
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

    }
}