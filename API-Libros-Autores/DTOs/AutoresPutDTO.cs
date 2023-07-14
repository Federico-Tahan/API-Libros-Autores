using API_Libros_Autores.Entidades;
using System.ComponentModel.DataAnnotations;

namespace API_Libros_Autores.DTOs
{
    public class AutoresPutDto : RespuestaBase
    {
        public AutoresPutDto()
        {
        }

        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres")]
        public string Nombre { get; set; }
    }
}
