using System.ComponentModel.DataAnnotations;

namespace API_Libros_Autores.Entidades
{
    public class Libro
    {
        public Libro()
        {
        }
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        public List<Comentario> Comentarios { get; set; }
        public List<AutorLibro> AutoresLibros { get; set; }

    }
}
