using FluentValidation;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Validations
{
    public class UpdateWalkDifficultyValidation : AbstractValidator<UpdateWalkDifficultyDTO>
    {
        public UpdateWalkDifficultyValidation()
        {
            RuleFor(wd => wd.Code).NotEmpty();
        }
    }
}
