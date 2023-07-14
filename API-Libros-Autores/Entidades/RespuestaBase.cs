using System.Net;

namespace API_Libros_Autores.Entidades
{
    public class RespuestaBase
    {
        public string Error { get; set; }
        public bool Exito { get; set; }
        public HttpStatusCode Codigo { get; set;}
    }
}
