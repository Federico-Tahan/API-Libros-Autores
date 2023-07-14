using API_Libros_Autores.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static API_Libros_Autores.CQRS.LibrosCQRS.Commands.PostLibro;
using static API_Libros_Autores.CQRS.LibrosCQRS.Queries.GetLibroById;
using static API_Libros_Autores.CQRS.LibrosCQRS.Queries.GetLibros;

namespace API_Libros_Autores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LibrosController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("/CargarLibro")]
        public async Task<LibroDTO> PostLibro([FromBody] PostLibroCommand c) 
        {
            return await _mediator.Send(c);
        }

        [HttpGet]
        [ResponseCache(Duration = 10)]
        [Route("/traerLibros")]

        public async Task<List<LibroDTO>> GetLibros()
        {
            return await _mediator.Send(new GetLibrosQueries { });
        }

        [HttpGet]
        [Route("{libroId:int}")]

        public async Task<LibroDTO> GetLibros(int libroId)
        {
            return await _mediator.Send(new GetLibroByIdQuerie { Id = libroId });
        }
    }
}
