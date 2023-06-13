using Domain.Entities;
using FluentValidation;

namespace Application.Validations
{
    public class RoomClassValidation:AbstractValidator<RoomClass>
    {
        public RoomClassValidation() 
        {
            RuleFor(roomclass => roomclass.Name)
                .Must(BeValidClassType)
                .WithMessage("Invalid RoomClass Type");
        }
        private bool BeValidClassType(string roomClass)
        {
            var validClassTypes = new[] { "Standart", "Deluxe", "Suite", "PresedentialSuite", "Connecting", "Adjacent" };
            return validClassTypes.Contains(roomClass);
        }
    }
}
