using FluentValidation;
using Spg.AutoTeileShop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AutoTeileShop.Application.Validators
{
    public class CarDtoValidator : AbstractValidator<CarDTO>
    {
        public CarDtoValidator()
        {
            RuleFor(c => c.Modell)
                .Length(1,25)
                .WithMessage("Bitte zwischen 1 und 25!")
                .WithErrorCode("9000");

            RuleFor(c => c.Marke)
               .Length(1, 25)
               .WithMessage("Bitte zwischen 1 und 25!")
               .WithErrorCode("9001");

            RuleFor(c => c.Baujahr)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Ein Baujahr kann nicht in der Zukunft liegen!")
                .WithErrorCode("9002");
            
        }
    }
}
