using System.ComponentModel.DataAnnotations;

namespace Casino_ProyectoFinal.DTOs
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
