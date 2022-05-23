namespace Casino_ProyectoFinal.Entidades
{
    public class ParticipantesRifa
    {
        public int ParticipanteId { get; set; }
        public int RifaId { get; set; }
        public int Orden { get; set; }
        public Participantes Participantes {get; set;}
        public Rifas Rifas { get; set; }
    }
}
