using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Payment
{
    public class PaymentGetDto:PaymentBaseDto
    {
        public Guid PaymentId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
