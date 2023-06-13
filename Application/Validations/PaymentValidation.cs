using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations
{
    public class PaymentValidation:AbstractValidator<Payment>
    {
        public PaymentValidation()
        {
            RuleFor(payment => payment.Type)
                .Must(BeValidPaymentType)
                .WithMessage("Invalid payment type");
        }

        private bool BeValidPaymentType(string paymentType)
        {
            var validPaymentTypes = new[] { "CreditCard", "Cash" };

            return validPaymentTypes.Contains(paymentType);
        }
    }
}
