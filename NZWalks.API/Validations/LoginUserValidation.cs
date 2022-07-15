using FluentValidation;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Validations
{
    public class LoginUserValidation : AbstractValidator<LoginUserDTO>
    {
        public LoginUserValidation()
        {
            RuleFor(u => u.Username).NotEmpty();
            RuleFor(u => u.Password).NotEmpty();
        }
    }
}
