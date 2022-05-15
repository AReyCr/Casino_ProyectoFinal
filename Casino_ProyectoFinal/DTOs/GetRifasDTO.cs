using System.ComponentModel.DataAnnotations;

namespace Casino_ProyectoFinal.DTOs
{
    public class GetRifasDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 15, ErrorMessage = "El campo {0} solo puede tener un limite de 15 caracteres")]
        public string Nombre { get; set; }
        public int NumerosDisponible { get; set; }
    }
}
