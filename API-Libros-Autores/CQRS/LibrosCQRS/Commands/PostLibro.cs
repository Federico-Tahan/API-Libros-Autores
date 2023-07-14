using API_Libros_Autores.CQRS.LibrosCQRS.Validator;
using API_Libros_Autores.DTOs;
using API_Libros_Autores.Entidades;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API_Libros_Autores.CQRS.LibrosCQRS.Commands
{
    public class PostLibro
    {
        public class PostLibroCommand : IRequest<LibroDTO>
        {
            public string Titulo { get; set; }
            public List<int> AutoresIds { get; set; }
        }


        public class PostLibroCommandHandler : IRequestHandler<PostLibroCommand, LibroDTO> 
        {
            private readonly ApplicationContext _context;
            private readonly PostLibroValidator _validator;
            private readonly IMapper _mapper;

            public PostLibroCommandHandler(ApplicationContext context, PostLibroValidator validator, IMapper map)
            {
                _context = context;
                _validator = validator;
                _mapper = map;
            }

            public async Task<LibroDTO> Handle(PostLibroCommand request, CancellationToken cancellationToken)
            {
                _validator.Validate(request);
                try 
                {
                    var autores = await _context.Autores.Where(p => request.AutoresIds.Contains(p.Id)).Select(p => p.Id).ToListAsync();
                    if (autores.Count == request.AutoresIds.Count)
                    {

                        var libro = _mapper.Map<Libro>(request);
                        await _context.AddAsync(libro);
                        await _context.SaveChangesAsync();
                        var libros = await _context.Libros.Include(p => p.AutoresLibros).ThenInclude(p => p.Autor).FirstOrDefaultAsync(p =>p.Titulo == request.Titulo );
                        return _mapper.Map<LibroDTO>(libros);
                    }
                    else 
                    {
                        throw new Exception("Está ingresando un autor que no existe en la BD");
                    }
                }
                catch
                (Exception ex)
                {
                    throw new Exception("Error al intentar guardar el libro");

                }
            }
        }
    }
}
