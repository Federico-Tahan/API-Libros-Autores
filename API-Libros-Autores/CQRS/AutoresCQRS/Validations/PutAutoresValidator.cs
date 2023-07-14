using API_Libros_Autores.DTOs;
using FluentValidation;
using static API_Libros_Autores.CQRS.AutoresCQRS.Commands.PutAutor;

namespace API_Libros_Autores.CQRS.AutoresCQRS.Validations
{
    public class PutAutoresValidator : AbstractValidator<PutAutorCommand>
    {
        public PutAutoresValidator()
        {
            RuleFor(p => p.Id).NotEmpty().NotNull();

            RuleFor(p => p.Nombre).NotEmpty().NotNull().MinimumLength(4).MaximumLength(150);
        }
    }
}
