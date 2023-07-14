using FluentValidation;
using static API_Libros_Autores.CQRS.LibrosCQRS.Commands.PostLibro;

namespace API_Libros_Autores.CQRS.LibrosCQRS.Validator
{
    public class PostLibroValidator : AbstractValidator<PostLibroCommand>
    {
        public PostLibroValidator()
        {
            RuleFor(p => p.AutoresIds).NotEmpty().NotNull();
            RuleFor(p => p.Titulo).NotEmpty().NotNull().MinimumLength(3).MaximumLength(70);
        }
    }
}
