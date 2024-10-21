using FluentValidation;

namespace ApiTemplate.Api.Models
{
    public class UserAuthenticationValidator : AbstractValidator<UserAuthentication>
    {
        public UserAuthenticationValidator()
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
