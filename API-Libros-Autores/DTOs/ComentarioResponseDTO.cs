using API_Libros_Autores.Entidades;

namespace API_Libros_Autores.DTOs
{
    public class ComentarioResponseDTO
    {
        public string Comentarios { get; set; }
        public LibroDTO Libro { get; set; }
    }
}
