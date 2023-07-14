using API_Libros_Autores.CQRS.AutoresCQRS.Commands;
using API_Libros_Autores.DTOs;
using API_Libros_Autores.Entidades;
using AutoMapper;
using static API_Libros_Autores.CQRS.AutoresCQRS.Commands.PostAutores;
using static API_Libros_Autores.CQRS.ComentariosCQRS.Command.PostComentario;
using static API_Libros_Autores.CQRS.LibrosCQRS.Commands.PostLibro;

namespace API_Libros_Autores.AutoMapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<AutoresDTO, Autor>();
            CreateMap<Autor, AutoresDTO>()
                .ForMember(autor => autor.LibroDTOs, opciones => opciones.MapFrom(MapLibrosAutores));

            CreateMap<AutoresPutDto, Autor>().ReverseMap();

            CreateMap<LibroDTO, Libro>();
            CreateMap<Libro, LibroDTO>().ForMember(libro => libro.Autores, opciones => opciones.MapFrom(MapAutoresLibros));

            CreateMap<PostLibroCommand, Libro>()
                .ForMember(libro => libro.AutoresLibros, opciones => opciones.MapFrom(MapAutoresLibros));

            CreateMap<PostAutoresCommand,Autor>().ReverseMap();
            CreateMap<PutAutor, Autor>().ReverseMap();

            CreateMap<ComentarioResponseDTO, Comentario>().ReverseMap();
            CreateMap<PostComentarioCommand, Comentario>().ReverseMap();
        }

        private List<LibroDTO> MapLibrosAutores(Autor autor, AutoresDTO autorDTO)
        {
            var resultado = new List<LibroDTO>();

            if (autor.AutoresLibros == null)
            {
                return resultado;
            }

            foreach (var autorLibro in autor.AutoresLibros)
            {
                resultado.Add(new LibroDTO()
                {
                    Titulo = autorLibro.Libro.Titulo

                });

            }
            return resultado;
        }

        private List<AutoresDTO> MapAutoresLibros(Libro libro, LibroDTO librodto)
        {
            var resultado = new List<AutoresDTO>();

            if (libro.AutoresLibros == null) 
            {
                return resultado;
            }

            foreach (var autorLibro in libro.AutoresLibros) 
            {
                resultado.Add(new AutoresDTO()
                {
                    Nombre = autorLibro.Autor.Nombre

                });
            
            }

            return resultado;
        }

        private List<AutorLibro> MapAutoresLibros(PostLibroCommand librocreacion, Libro libro)
        {
            var resultado = new List<AutorLibro>();

            if (librocreacion.AutoresIds == null) 
            {
               return resultado;
            }

            foreach(var autorId in librocreacion.AutoresIds)
            {
                resultado.Add(new AutorLibro() { AutorId = autorId });
            }

            return resultado;
        }
    }
}
