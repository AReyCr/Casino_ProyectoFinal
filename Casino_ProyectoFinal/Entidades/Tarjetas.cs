namespace Casino_ProyectoFinal.Entidades
{
    public class Tarjetas
    { 
        public int Id { get; set; }
        public int RifaId { get; set; }
        public Rifas Rifas { get; set; }
  
        public int ParticipanteId { get; set; }
        public Participantes Participantes { get; set; }
    }
}
