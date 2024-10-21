using ApiTemplate.Application.Validators;
using FluentValidation.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace ApiTemplate.Application.Commands
{
    public class GenerateTokenCommand: IRequest<GenerateTokenCommandResponse>
    {

        [JsonIgnore]
        public ValidationResult? ValidationResult { get; private set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public bool IsValid()
        {
            ValidationResult = new GenerateTokenCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
