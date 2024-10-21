using ApiTemplate.Application.Commands;
using FluentValidation;

namespace ApiTemplate.Application.Validators
{
    public class GenerateTokenCommandValidator : AbstractValidator<GenerateTokenCommand>
    {
        public GenerateTokenCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo {PropertyName} é obrigatório")
                .MaximumLength(200)
                .WithMessage("O campo {PropertyName} deve ter até 200 caracteres");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo {PropertyName} é obrigatório")
                .MaximumLength(200)
                .WithMessage("O campo {PropertyName} deve ter até 200 caracteres");

        }
    }
}
