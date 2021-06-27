using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace EventsAPI.Models
{
    public class EventValidator : AbstractValidator<EventModel>
    {

        public EventValidator()
        {
            RuleFor(m => m.Name)
               .NotEmpty()
               .MaximumLength(500);

            RuleFor(m => m.Description)
               .NotEmpty();

            RuleFor(m => m.Timezone)
               .MaximumLength(255);

            RuleFor(m => m.StartDate)
               .NotEmpty().WithMessage("Start Date is required");

            RuleFor(r => r.EndDate)
               .NotEmpty().WithMessage("End Date is required")
               .GreaterThan(r => r.StartDate)
               .WithMessage("End date must be after Start date");


        }
    }
}
