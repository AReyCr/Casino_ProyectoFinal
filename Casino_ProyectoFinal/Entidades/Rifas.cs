using System.ComponentModel.DataAnnotations;

namespace Casino_ProyectoFinal.Entidades
{
    public class Rifas : IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 15, ErrorMessage = "El campo {0} solo puede tener un limite de 15 caracteres")]
       
        public string Nombre { get; set;}
        public int NumerosDisponible { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {
                var primeraLetra = Nombre[0].ToString();
                if(primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayuscula", new String[] { nameof(Nombre) });
                }
            }
        }
    }
}
