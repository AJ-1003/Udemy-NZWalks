using FluentValidation;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Validations
{
    public class CreateWalkDifficultyValidation : AbstractValidator<CreateWalkDifficultyDTO>
    {
        public CreateWalkDifficultyValidation()
        {
            RuleFor(wd => wd.Code).NotEmpty();
        }
    }
}
