using System.ComponentModel.DataAnnotations;

namespace Casino_ProyectoFinal.DTOs
{
    public class CredencialUsuario
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password  { get; set; }
    }
}
