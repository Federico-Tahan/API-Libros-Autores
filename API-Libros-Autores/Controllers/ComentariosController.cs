using API_Libros_Autores.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static API_Libros_Autores.CQRS.ComentariosCQRS.Command.PostComentario;
using static API_Libros_Autores.CQRS.ComentariosCQRS.Queries.GetComentariosByIdLibro;

namespace API_Libros_Autores.Controllers
{
    [Route("api/libros/{libroId:int}/comentarios")]
    [ApiController]
    public class ComentariosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ComentariosController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("/api/libros/PostComentario")]
        public async Task<ActionResult<ComentarioResponseDTO>> PostComentario([FromBody] PostComentarioCommand comentario) 
        {
            return Ok(await _mediator.Send(comentario));
        }

        [HttpGet]
        public async Task<ActionResult<ComentarioResponseDTO>> GetComentariosByIDLibro(int libroId)
        {
            return Ok(await _mediator.Send(new GetComentariosByIdLibroQuerie {Id =libroId }));
        }
    }
}
