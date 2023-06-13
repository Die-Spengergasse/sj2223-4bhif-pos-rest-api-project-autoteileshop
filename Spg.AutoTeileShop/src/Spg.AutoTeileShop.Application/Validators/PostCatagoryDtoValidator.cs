using FluentValidation;
using Spg.AutoTeileShop.Domain.DTO;

namespace Spg.AutoTeileShop.Application.Validators
{
    public class PostCatagoryDtoValidator : AbstractValidator<CatagoryPostDTO>
    {
        public PostCatagoryDtoValidator()
        {
            RuleFor(c => c.Name)
                .Length(1, 30)
                .WithMessage("Bitte zwischen 1 und 30!")
                .WithErrorCode("9000");
            RuleFor(c => c.Description).NotEmpty().WithMessage("Bitte Beschreibung angeben!").WithErrorCode("9000");
            RuleFor(c => c.Description)
                .Length(1, 30)
                .WithMessage("Bitte zwischen 1 und 30!")
                .WithErrorCode("9000");

        }
    }
}
