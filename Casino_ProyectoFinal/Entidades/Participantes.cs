namespace Casino_ProyectoFinal.Entidades
{
    public class Participantes
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int NumeroSeleccion { get; set; }
        public int RifasId { get; set; }
        public int RegistroId { get; set; }
        public Rifas Rifas { get; set; }
        public Registro Registro { get; set; }
    }
}
