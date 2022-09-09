using FluentValidation;
using LibraryStore.Models;

namespace LibraryStore.Business.Models.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(c => c.Name)
               .NotEmpty().WithMessage("O campo Nome precisa ser fornecido")
               .Length(2, 200).WithMessage("O campo Nome precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("O campo Descrição precisa ser fornecido")
                .Length(2, 1000).WithMessage("O campo Descrição precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Price)
                .GreaterThan(0).WithMessage("O campo Valor precisa ser maior que {ComparisonValue}");
        }
    }
}
