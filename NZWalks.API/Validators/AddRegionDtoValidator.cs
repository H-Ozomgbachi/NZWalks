using FluentValidation;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Validators
{
    public class AddRegionDtoValidator : AbstractValidator<AddRegionDTO>
    {
        public AddRegionDtoValidator()
        {
            RuleFor(r => r.Code).NotEmpty().WithMessage("Code cannot be null or whitespace");
            RuleFor(r => r.Name).NotEmpty().WithMessage("Name cannot be null or whitespace");
            RuleFor(r => r.Area).GreaterThan(0);
            RuleFor(r => r.Population).GreaterThanOrEqualTo(0);
        }
    }
}
