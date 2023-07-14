using API_Libros_Autores.Entidades;

namespace API_Libros_Autores.DTOs
{
    public class LibroDTO:RespuestaBase
    {
        public LibroDTO()
        {
        }

        public string Titulo { get; set; }
        public List<AutoresDTO> Autores { get; set; }
       /* public List<ComentarioResponseDTO> Comentarios { get; set; }*/

    }
}
