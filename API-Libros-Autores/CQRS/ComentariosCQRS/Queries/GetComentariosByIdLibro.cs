using API_Libros_Autores.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API_Libros_Autores.CQRS.ComentariosCQRS.Queries
{
    public class GetComentariosByIdLibro
    {
        public class GetComentariosByIdLibroQuerie : IRequest <LibroDTO>
        {
            public int Id { get; set; }
        }


        public class GetComentariosByIdLibroQuerieHandler : IRequestHandler<GetComentariosByIdLibroQuerie, LibroDTO>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper mapper;

            public GetComentariosByIdLibroQuerieHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                this.mapper = mapper;
            }

            public async Task<LibroDTO> Handle(GetComentariosByIdLibroQuerie request, CancellationToken cancellationToken)
            {
                try 
                {
                    var libro = await _context.Libros.Include(c => c.Comentarios).FirstOrDefaultAsync(p => p.Id == request.Id);

                    if (libro == null) 
                    {
                        throw new Exception("El libro con ID "+request.Id + " No existe" );
                    }

                    return mapper.Map<LibroDTO>(libro);
                }
                catch (Exception ex) 
                {
                    throw new Exception("Error al buscar libro " + ex.Message);

                }
            }
        }
    }
}
