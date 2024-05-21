using ApiTemplate.Application.Commands;
using FluentValidation;

namespace ApiTemplate.Application.Validators
{
    public class UserAddCommandValidator: AbstractValidator<UserAddCommand>
    {
        public UserAddCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo {PropertyName} é obrigatório")
                .MaximumLength(200)
                .WithMessage("O campo {PropertyName} deve ter até 200 caracteres");

            RuleFor(x => x.Email)
             .NotNull()
             .NotEmpty()
             .WithMessage("O campo {PropertyName} é obrigatório")
             .MaximumLength(100)
             .WithMessage("O campo {PropertyName} deve ter até 200 caracteres")
             .EmailAddress()
             .WithMessage("Email inválido");

        }
    }
}
