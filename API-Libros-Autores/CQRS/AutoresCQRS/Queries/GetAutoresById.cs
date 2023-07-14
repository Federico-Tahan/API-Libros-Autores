using API_Libros_Autores.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API_Libros_Autores.CQRS.AutoresCQRS.Queries
{
    public class GetAutoresById
    {
        public class GetAutoresByIdQuery : IRequest<AutoresDTO> 
        {
            public int Id { get; set; }
        }

        public  class GetAutoresByIdQueryHandler : IRequestHandler<GetAutoresByIdQuery,AutoresDTO> 
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;

            public GetAutoresByIdQueryHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<AutoresDTO> Handle(GetAutoresByIdQuery request, CancellationToken cancellationToken)
            {
                try 
                {
                    var autor = await _context.Autores.Include(p => p.AutoresLibros).ThenInclude(p => p.Libro).FirstOrDefaultAsync(p => p.Id == request.Id);
                    if (autor != null)
                    {

                        return _mapper.Map<AutoresDTO>(autor);


                    }
                    else 
                    {
                        throw new Exception("Autor con ID " + request.Id + " No encontrado");
                    }
                      

                }catch (Exception ex) 
                {
                    throw new Exception("Error al buscar autores por ID");

                }
            }
        }
    }
}
