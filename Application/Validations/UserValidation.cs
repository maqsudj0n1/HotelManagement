using Domain.Entities;
using FluentValidation;

namespace Application.Validations;

public class UserValidation:AbstractValidator<Employee>
{
    public UserValidation()
    {
        RuleFor(x=>x.Username)
        .NotEmpty()
        .NotNull()
        .MaximumLength(20)
        .MinimumLength(5)
        .WithMessage("Username is not valid");

        RuleFor(x => x.FullName)
       .NotEmpty()
       .NotNull()
       .MaximumLength(20)
       .MinimumLength(5)
       .WithMessage("FullName is not valid");

        RuleFor(x => x.Password)
       .NotEmpty()
       .NotNull()
       .Matches("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$")
       .WithMessage("Password is not valid")
       .MinimumLength(6)
       .WithMessage("Password is not valid");

        RuleFor(x => x.PhoneNumber)
       .Matches(@"^\+998\d{9}$")
       .WithMessage("PhoneNumbers is not valid");
    }
}
