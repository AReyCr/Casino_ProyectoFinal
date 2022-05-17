using Casino_ProyectoFinal.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace Casino_ProyectoFinal.DTOs
{
    public class ParticipantesPatchDTO
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 15, ErrorMessage = "El campo {0} solo puede tener un limite de 15 caracteres")]
        [Mayuscula]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {1} es requerido")]
        [Range(1, 54, ErrorMessage = "El numero selecionado no entra en el rango")]
        public int NumeroSeleccion { get; set; }

        

        
    }
}
