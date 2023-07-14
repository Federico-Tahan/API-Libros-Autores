
using API_Libros_Autores.CQRS.AutoresCQRS.Validations;
using API_Libros_Autores.DTOs;
using API_Libros_Autores.Entidades;
using AutoMapper;
using MediatR;
using System.Net;

namespace API_Libros_Autores.CQRS.AutoresCQRS.Commands
{
    public class PostAutores
    {

        public class PostAutoresCommand : IRequest<AutoresDTO> 
        {
            public string Nombre { get; set; }
        }


        public class PostAutoresCommandHandler : IRequestHandler<PostAutoresCommand, AutoresDTO>
        {
            private readonly ApplicationContext _context;
            private readonly PostAutoresValidator _validator;
            private readonly IMapper _mapper;

            public PostAutoresCommandHandler(ApplicationContext context, PostAutoresValidator validator,IMapper mapper)
            {
                _context = context;
                _validator = validator;
                _mapper = mapper;
            }

            public async Task<AutoresDTO> Handle(PostAutoresCommand request, CancellationToken cancellationToken)
            {
                AutoresDTO autDTO = new AutoresDTO();
                _validator.Validate(request);
                try 
                {
                    var autor = _mapper.Map<Autor>(request);
                    await _context.AddAsync(autor);
                    await _context.SaveChangesAsync();
                    autDTO = _mapper.Map<AutoresDTO>(autor);
                    autDTO.Exito = true;
                    autDTO.Codigo = HttpStatusCode.OK;

                }catch (Exception ex) 
                {
                    autDTO.Error = ex.Message;
                    autDTO.Exito = false;
                    autDTO.Codigo = HttpStatusCode.NotImplemented;
                }

                return autDTO;
            }
        }
    }
}
