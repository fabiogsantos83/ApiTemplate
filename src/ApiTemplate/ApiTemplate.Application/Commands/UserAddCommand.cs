using ApiTemplate.Application.Validators;
using FluentValidation.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace ApiTemplate.Application.Commands
{
    public class UserAddCommand : IRequest<Guid>
    {
        [JsonIgnore]
        public ValidationResult? ValidationResult { get; private set; }

        public string Name { get; set; }
        public string Email { get; set; }

        public bool IsValid()
        {
            ValidationResult = new UserAddCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
