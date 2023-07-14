namespace API_Libros_Autores.Entidades
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Comentarios { get; set; }
        public int LibroId { get; set; }
        public Libro Libro { get; set; } 
    }
}
