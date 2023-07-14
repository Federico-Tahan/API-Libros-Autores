using API_Libros_Autores.CQRS.LibrosCQRS.Validator;
using API_Libros_Autores.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API_Libros_Autores.CQRS.LibrosCQRS.Queries
{
    public class GetLibroById
    {
        public class GetLibroByIdQuerie : IRequest<LibroDTO> 
        {
            public int Id { get; set; }
        }


        public class GetLibroByIdQuiereHandler : IRequestHandler<GetLibroByIdQuerie, LibroDTO> 
        {
            private readonly ApplicationContext _context;
            private readonly GetLibroByIdValidator _validator;
            private readonly IMapper _mapper;
            public GetLibroByIdQuiereHandler(ApplicationContext context, GetLibroByIdValidator validator, IMapper mapper)
            {
                _context = context;
                _validator = validator;
                _mapper = mapper;
            }

            public async Task<LibroDTO> Handle(GetLibroByIdQuerie request, CancellationToken cancellationToken)
            {
                
                _validator.Validate(request);
                LibroDTO libroDTO = new LibroDTO();

                try
                {
                    var libro = await _context.Libros.Include(c => c.AutoresLibros).ThenInclude(c => c.Autor).FirstOrDefaultAsync(p => p.Id == request.Id);

                    if (libro != null)
                    {
                        
                        libroDTO = _mapper.Map<LibroDTO>(libro);

                        libroDTO.Exito = true;
                        libroDTO.Codigo = HttpStatusCode.OK;

                    }
                    else 
                    {
                        throw new Exception("Libro con ID " + request.Id + " No encontrado");

                    }

                }
                catch (Exception ex)
                {

                    libroDTO.Error = ex.Message;
                    libroDTO.Exito = false;
                    libroDTO.Codigo = HttpStatusCode.NotImplemented;
                }
                return libroDTO;
            }
        }
    }
}
