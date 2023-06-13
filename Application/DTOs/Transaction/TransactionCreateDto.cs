using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Transaction
{
    public class TransactionCreateDto:TransactionBaseDto
    {
        public Guid CustomerId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid ReservationId { get; set; }
    }
}
