using System.ComponentModel.DataAnnotations;

namespace Casino_ProyectoFinal.DTOs
{
    public class RifasDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 15, ErrorMessage = "El campo {0} solo puede tener un limite de 15 caracteres")]
        public string Nombre { get; set; }

        public int[] NumerosDisponible = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54 };
        public int[] NoDisponible = new int[] { 0 };
    }
}
