using API_Libros_Autores.Entidades;
using System.ComponentModel.DataAnnotations;

namespace API_Libros_Autores.DTOs
{
    public class AutoresDTO : RespuestaBase
    {
        public AutoresDTO()
        {
        }

        [Required]
        [StringLength(maximumLength: 150, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres")]
        public string Nombre { get; set; }
        public List<LibroDTO> LibroDTOs { get; set; }


    }
}
