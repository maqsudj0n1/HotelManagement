using Domain.Entities;
using FluentValidation;

namespace Application.Validations
{
    public class RoomValidation:AbstractValidator<Room>
    {
        public RoomValidation()
        {
            RuleFor(room => room.Price)
                .NotEmpty().WithMessage("Price is required.")
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
            RuleFor(room => room.Description)
           .NotEmpty().WithMessage("Description is required.")
           .MaximumLength(300).WithMessage("Description cannot exceed 100 characters.");
        }


    }
}
