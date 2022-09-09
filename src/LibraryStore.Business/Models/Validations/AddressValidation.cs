using FluentValidation;
using LibraryStore.Models;

namespace LibraryStore.Business.Models.Validations
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(c => c.AddressPlace)
                .NotEmpty().WithMessage("O campo Logradouro precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo Logradouro precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Neighborhood)
                .NotEmpty().WithMessage("O campo Bairro precisa ser fornecido")
                .Length(2, 100).WithMessage("O campo Bairro precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.ZipCode)
                .NotEmpty().WithMessage("O campo CEP precisa ser fornecido")
                .Length(8).WithMessage("O campo CEP precisa ter {MaxLength} caracteres");

            RuleFor(c => c.City)
                .NotEmpty().WithMessage("A campo Cidade precisa ser fornecida")
                .Length(2, 100).WithMessage("O campo Cidade precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.State)
                .NotEmpty().WithMessage("O campo Estado precisa ser fornecido")
                .Length(2, 50).WithMessage("O campo Estado precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.NumberAddress)
                .NotEmpty().WithMessage("O campo Numero precisa ser fornecido")
                .Length(1, 50).WithMessage("O campo Numero precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Complement)
               .NotEmpty().WithMessage("O campo Complemento precisa ser fornecido")
               .Length(2, 100).WithMessage("O campo Complemento precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
