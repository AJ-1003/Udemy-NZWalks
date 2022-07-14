using FluentValidation;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Validations
{
    public class CreateWalkValidation : AbstractValidator<CreateWalkDTO>
    {
        public CreateWalkValidation()
        {
            RuleFor(w => w.Name).NotEmpty();
            RuleFor(w => w.Length).GreaterThan(0);
        }
    }
}
