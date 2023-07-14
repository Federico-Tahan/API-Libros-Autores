using API_Libros_Autores.CQRS.ComentariosCQRS.Validator;
using API_Libros_Autores.DTOs;
using API_Libros_Autores.Entidades;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API_Libros_Autores.CQRS.ComentariosCQRS.Command
{
    public class PostComentario
    {
        public class PostComentarioCommand : IRequest<ComentarioResponseDTO>
        {
            public string Comentarios { get; set; }
            public int LibroId { get; set; }
        }

        public class PostComentarioCommandHandler : IRequestHandler<PostComentarioCommand, ComentarioResponseDTO>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            private readonly PostComentarioValidator _validator;

            public PostComentarioCommandHandler(ApplicationContext context, IMapper mapper, PostComentarioValidator validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }

            public async Task<ComentarioResponseDTO> Handle(PostComentarioCommand request, CancellationToken cancellationToken)
            {
                _validator.Validate(request);
                ComentarioResponseDTO comentarioResponseDTO = new ComentarioResponseDTO();
                try 
                {
                    var libroban = await _context.Libros.AnyAsync(p =>p.Id == request.LibroId);

                    if (!libroban) 
                    {
                        throw new Exception("El ID " + request.LibroId + " No pertenece a ningun libro registrado en la BD");
                    }

                    var comentario = _mapper.Map<Comentario>(request);
                    await _context.AddAsync(comentario);
                    await _context.SaveChangesAsync();
                    var libro = await _context.Libros.FirstOrDefaultAsync(a => a.Id == request.LibroId);
                    comentarioResponseDTO = _mapper.Map<ComentarioResponseDTO>(comentario);
                    comentarioResponseDTO.Libro = _mapper.Map<LibroDTO>(libro);


                }
                catch (Exception ex) 
                {
                    throw new Exception("Error al intentar cargar un comentario ERROR: "+ ex.Message);

                }
                return comentarioResponseDTO;

            }
        }
    }
}
