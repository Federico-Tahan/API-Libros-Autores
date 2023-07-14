using FluentValidation;
using System.Data;
using static API_Libros_Autores.CQRS.ComentariosCQRS.Command.PostComentario;

namespace API_Libros_Autores.CQRS.ComentariosCQRS.Validator
{
    public class PostComentarioValidator : AbstractValidator<PostComentarioCommand>
    {
        public PostComentarioValidator()
        {
            RuleFor(p => p.LibroId).NotEmpty().NotNull().WithMessage("Debe ingresar un ID de libro");
            RuleFor(p => p.Comentarios).NotEmpty().WithMessage("Debe ingresar el comentario del libro");
        }
    }
}
