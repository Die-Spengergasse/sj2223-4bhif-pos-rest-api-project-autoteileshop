using FluentValidation;
using Spg.AutoTeileShop.Domain.DTO;

namespace Spg.AutoTeileShop.Application.Validators
{
    public class NewProductDtoValidator : AbstractValidator<ProductDTO>
    {
        public NewProductDtoValidator()
        {
            RuleFor(p => p.Name)
                .Length(3, 20)
                .WithMessage("Bitte zwischen 3 und 20!")
                .WithErrorCode("9000");

            RuleFor(p => p.Ean13).Length(13).WithMessage("Bitte 13 Zeichen!");
        }
    }
}
