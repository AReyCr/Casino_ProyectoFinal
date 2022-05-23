using AutoMapper;
using Casino_ProyectoFinal.DTOs;
using Casino_ProyectoFinal.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Casino_ProyectoFinal.Controllers;
using System.Collections.Generic;

namespace Casino_ProyectoFinal.Controllers
{
    [ApiController]
    [Route("api/Participantes")]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)] 
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
        public async Task<ActionResult> Post ( ParticipantesDTO participantesDto//[FromQuery]RifasDTO rifasDto
                                                                         )
        {
            var repetido = await dbContext.Participantes.AnyAsync(x => x.NumeroSeleccion == participantesDto.NumeroSeleccion);
            var rifa = await dbContext.Participantes.AnyAsync(y => y.RifasId == participantesDto.RifasId);

            if (repetido && rifa)
            {
                return BadRequest("Numero ya selecionado");
            }


            /*
             else
             {
                 for (int i = 0; i < 54; i++)
                 {
                     if (participantesDto.NumeroSeleccion == rifasDto.NumerosDisponible[i])
                     {
                         var w = rifasDto.NumerosDisponible[i];
                        dbContext.Rifas.Remoeeve(w);
                     }
                 }
             }
             */

           // var resta = await dbContext.Rifas.AnyAsync(rifasDto.Disponible - 1);

            var participante = mapper.Map<Participantes>(participantesDto);

            dbContext.Add(participante);
            await dbContext.SaveChangesAsync();

            var participantesDTO = mapper.Map<GetParticipantesDTO>(participante);

            return Ok();
        }

        [HttpGet("ConsultaRifa")]
        public async Task<ActionResult<List<GetRifasDTO>>> GetAll()
        {
            var rifas = await dbContext.Rifas.ToListAsync();
            return mapper.Map<List<GetRifasDTO>>(rifas);
        }

        [HttpGet("NumerosDisponibles")]  // OBTENER SOLAMENTE DISPONIBLES
        public async Task<ActionResult<RifasDTO>> Get(int id)
        {
            var rifa = await dbContext.Rifas.FirstOrDefaultAsync(x => x.Id == id);
            if (rifa == null)
            {
                return NotFound();
            }

            return mapper.Map<RifasDTO>(rifa.NumerosDisponible[1]);

        }

        /*
        [HttpGet( Name="ObtenerParticipantes")]    // Get para datos relacionados ( RIFAS )
        //[AllowAnonymous] No pedir autorizacion 
        public async Task<ActionResult<List<GetParticipantesDTO>>> Get()
        {
            logger.LogInformation("Se obtiene el listado de los participantes");
            logger.LogWarning("Mensaje de prueba warning");
            var participantes = await dbContext.Participantes.Include(x => x.Rifas).ToListAsync();
            return mapper.Map<List<GetParticipantesDTO>>(participantes);
        }



        
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
          */


       
       

    }
}