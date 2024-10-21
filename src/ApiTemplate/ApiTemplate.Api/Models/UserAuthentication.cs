using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace ApiTemplate.Api.Models
{
    public class UserAuthentication
    {
        [JsonIgnore]
        public ValidationResult? ValidationResult { get; private set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public bool IsValid()
        {
            ValidationResult = new UserAuthenticationValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
