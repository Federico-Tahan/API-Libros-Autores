using FluentValidation;
using static API_Libros_Autores.CQRS.LibrosCQRS.Queries.GetLibroById;

namespace API_Libros_Autores.CQRS.LibrosCQRS.Validator
{
    public class GetLibroByIdValidator : AbstractValidator<GetLibroByIdQuerie>
    {
        public GetLibroByIdValidator()
        {
            RuleFor(p => p.Id).NotEmpty().NotNull();
        }
    }
}
