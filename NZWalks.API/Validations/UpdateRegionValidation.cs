using FluentValidation;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Validations
{
    public class UpdateRegionValidation : AbstractValidator<UpdateRegionDTO>
    {
        public UpdateRegionValidation()
        {
            RuleFor(r => r.Code).NotEmpty();
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Area).GreaterThan(0);
            RuleFor(r => r.Latitude).GreaterThanOrEqualTo(-90).LessThanOrEqualTo(90);
            RuleFor(r => r.Longitude).GreaterThanOrEqualTo(-180).LessThanOrEqualTo(180);
            RuleFor(r => r.Population).GreaterThan(0);
        }
    }
}
