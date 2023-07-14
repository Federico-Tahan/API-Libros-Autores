using API_Libros_Autores.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static API_Libros_Autores.CQRS.AutoresCQRS.Commands.PostAutores;
using static API_Libros_Autores.CQRS.AutoresCQRS.Commands.PutAutor;
using static API_Libros_Autores.CQRS.AutoresCQRS.Queries.GetAutores;
using static API_Libros_Autores.CQRS.AutoresCQRS.Queries.GetAutoresById;

namespace API_Libros_Autores.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]

    public class AutoresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AutoresController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpPost]
        [Route("/crearAutor")]
        public async Task<AutoresDTO> PostAutores([FromBody] PostAutoresCommand autor) 
        {
            return await _mediator.Send(autor);
        }


        [HttpGet]
        [ResponseCache(Duration = 10)]
        [Route("/traerautor/{id}")]
        public async Task<AutoresDTO> GetAutores(int id)
        {
            return await _mediator.Send(new GetAutoresByIdQuery { Id = id });
        }

        [HttpGet]
        [ResponseCache(Duration = 10)]
        [Route("/traerAutores")]
        public async Task<List<AutoresDTO>> GetAutoresById()
        {
            return await _mediator.Send(new GetAutoresQueries { });
        }

        [HttpPut]
        [Route("/actualizarAutores")]
        public async Task<AutoresPutDto> PutAutores([FromBody] PutAutorCommand autor)
        {
            return await _mediator.Send(autor);
        }
    }
}
