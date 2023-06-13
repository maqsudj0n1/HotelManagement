using Domain.Entities;
using FluentValidation;

namespace Application.Validations
{
    public class CustomerValidation:AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(x => x.CustomerFullName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(20)
                .MinimumLength(9)
                .WithMessage("FullName is not Valid");
            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+998\d{9}$")
                .WithMessage("PhoneNumbers is not valid");
        }
    }
}
