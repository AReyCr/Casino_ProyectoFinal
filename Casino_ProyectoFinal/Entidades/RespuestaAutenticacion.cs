namespace Casino_ProyectoFinal.Entidades
{
    public class RespuestaAutenticacion
    {
        public int Id { get; set; }
        public string Token { get; set; }

        public DateTime Expiracion { get; set; }
    }
}
