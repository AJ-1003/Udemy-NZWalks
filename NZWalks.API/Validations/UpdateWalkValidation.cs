using FluentValidation;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Validations
{
    public class UpdateWalkValidation : AbstractValidator<UpdateWalkDTO>
    {
        public UpdateWalkValidation()
        {
            RuleFor(w => w.Name).NotEmpty();
            RuleFor(w => w.Length).GreaterThan(0);
        }
    }
}
