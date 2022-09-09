using FluentValidation;
using LibraryStore.Business.Models.Validations.Documents;
using LibraryStore.Models;

namespace LibraryStore.Business.Models.Validations
{
    public class ProviderValidation : AbstractValidator<Provider>
    {
        public ProviderValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("O campo Nome precisa ser fornecido")
                .Length(2, 100).WithMessage("O Campo Nome precisa ter entre {MinLength} e {MaxLength} caracteres");

            When(p => p.TypeProvider == TypeProvider.PhysicalPerson, () =>
            {
                RuleFor(p => p.Document.Length).Equal(CpfValidacao.TamanhoCpf)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}");

                RuleFor(p => CpfValidacao.Validar(p.Document)).Equal(true)
                    .WithMessage("O documento fornecido é inválido");
            });

            When(p => p.TypeProvider == TypeProvider.LegalPerson, () =>
            {
                RuleFor(p => p.Document.Length).Equal(CnpjValidacao.TamanhoCnpj)
                    .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}");

                RuleFor(p => CnpjValidacao.Validar(p.Document)).Equal(true)
                    .WithMessage("O documento fornecido é inválido");
            });
        }
    }
}
