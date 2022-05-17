using System.ComponentModel.DataAnnotations;

namespace Casino_ProyectoFinal.Entidades
{
    public class CredencialUsuario
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
