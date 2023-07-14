using API_Libros_Autores.CQRS.AutoresCQRS.Validations;
using API_Libros_Autores.DTOs;
using API_Libros_Autores.Entidades;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API_Libros_Autores.CQRS.AutoresCQRS.Commands
{
    public class PutAutor
    {
        public class PutAutorCommand : IRequest<AutoresPutDto> 
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
        }

        public class PutAutorCommandHandler : IRequestHandler<PutAutorCommand, AutoresPutDto> 
        {
            private readonly ApplicationContext _context;
            private readonly PutAutoresValidator _validator;
            private readonly IMapper _mapper;

            public PutAutorCommandHandler(ApplicationContext context, PutAutoresValidator validator, IMapper mapper)
            {
                _context = context;
                _validator = validator;
                _mapper = mapper;
            }

            public async Task<AutoresPutDto> Handle(PutAutorCommand request, CancellationToken cancellationToken)
            {
                AutoresPutDto dto = new AutoresPutDto();

                _validator.Validate(request);
                try
                {
                    var autor = await _context.Autores.FirstOrDefaultAsync(p => p.Id == request.Id);
                    if (autor != null)
                    {
                        autor = _mapper.Map<Autor>(request);
                        _context.Update(autor);
                        await _context.SaveChangesAsync();
                        dto = _mapper.Map<AutoresPutDto>(autor);
                        dto.Exito = true;
                        dto.Codigo = HttpStatusCode.OK;
                        return dto;
                    }
                    else 
                    {
                        throw new Exception("Autor con ID " + request.Id + " No encontrado.");
                    }
                }
                catch (Exception ex) 
                {
                    dto.Error = ex.Message;
                    dto.Exito = false;
                    dto.Codigo = HttpStatusCode.NotModified;
                }

                return dto;
            }
        }

    }
}
