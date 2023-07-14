using API_Libros_Autores.DTOs;
using API_Libros_Autores.Entidades;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API_Libros_Autores.CQRS.AutoresCQRS.Queries
{
    public class GetAutores
    {

        public class GetAutoresQueries : IRequest<List<AutoresDTO>> 
        {
        
        
        }


        public class GetAutoresQueriesHandler : IRequestHandler<GetAutoresQueries, List<AutoresDTO>>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;

            public GetAutoresQueriesHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<AutoresDTO>> Handle(GetAutoresQueries request, CancellationToken cancellationToken)
            {
                try
                {
                    var autores = await _context.Autores.ToListAsync();

                    if (autores != null)
                    {
                        List<AutoresDTO> listaAutores = new List<AutoresDTO>();

                        foreach (Autor autor in autores)
                        {
                            AutoresDTO dto = new AutoresDTO();
                            dto = _mapper.Map<AutoresDTO>(autor);
                            dto.Exito = true;
                            dto.Codigo = HttpStatusCode.OK;
                            listaAutores.Add(dto);
                        }
                        return listaAutores;
                    }
                    else
                    {
                        throw new Exception("No se han encontrado autores");
                    }
                }
                catch (Exception ex) 
                {
                    throw new Exception("Error al buscar autores");
                }
            }
        }
    }
}
