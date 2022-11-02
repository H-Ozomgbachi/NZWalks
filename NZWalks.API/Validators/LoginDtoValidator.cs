using FluentValidation;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDTO>
    {
        public LoginDtoValidator()
        {
            RuleFor(c => c.Password).NotEmpty();
            RuleFor(c => c.Username).NotEmpty();
        }
    }
}
