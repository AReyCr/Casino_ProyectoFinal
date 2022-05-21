using AutoMapper;
using Casino_ProyectoFinal.Entidades;
using Casino_ProyectoFinal.DTOs;

namespace Casino_ProyectoFinal.Utilidades
{
    public class AutoMapperProfiles : Profile
    { 
        public AutoMapperProfiles()
        {
            CreateMap<ParticipantesDTO, Participantes>();
            CreateMap<Participantes, GetParticipantesDTO>();
            CreateMap<RifasDTO, Rifas>();
            CreateMap<Rifas, GetRifasDTO>();
            CreateMap<Rifas, RifasDTO>();
            CreateMap<Participantes, ParticipantesPatchDTO>();
            CreateMap<Int32, RifasDTO>();
        }
    }
}
