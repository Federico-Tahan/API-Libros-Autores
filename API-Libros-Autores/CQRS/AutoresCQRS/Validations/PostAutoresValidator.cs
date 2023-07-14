using FluentValidation;
using static API_Libros_Autores.CQRS.AutoresCQRS.Commands.PostAutores;

namespace API_Libros_Autores.CQRS.AutoresCQRS.Validations
{
    public class PostAutoresValidator : AbstractValidator<PostAutoresCommand>
    {
        public PostAutoresValidator()
        {
            RuleFor(p => p.Nombre).NotEmpty().NotNull().MinimumLength(4).MaximumLength(20);
        }
    }
}
