using API_Libros_Autores.DTOs;
using API_Libros_Autores.Entidades;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API_Libros_Autores.CQRS.LibrosCQRS.Queries
{
    public class GetLibros
    {
        public class GetLibrosQueries : IRequest<List<LibroDTO>> 
        {
        
        }

        public class GetLibrosQueriesHandler : IRequestHandler<GetLibrosQueries, List<LibroDTO>> 
        {
            private readonly ApplicationContext _context;

            public GetLibrosQueriesHandler(ApplicationContext context)
            {
                this._context = context;
            }
            public async Task<List<LibroDTO>> Handle(GetLibrosQueries request, CancellationToken cancellationToken)
            {
                try 
                {
                    var listaLibros = await _context.Libros.ToListAsync();
                    List<LibroDTO> listaLibrosDTO = new List<LibroDTO>();
                    if (listaLibros != null) 
                    {
                        foreach (Libro l in listaLibros) 
                        {
                            LibroDTO libroDTO = new LibroDTO();
                            libroDTO.Titulo = l.Titulo;
                            listaLibrosDTO.Add(libroDTO);
                            
                        }
                    }
                    return listaLibrosDTO;
                }
                catch (Exception e) 
                {
                    throw new Exception("Problema al encontrar libros");
                }
            }
        }
    }
}
