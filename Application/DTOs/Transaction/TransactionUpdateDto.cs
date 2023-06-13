using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Transaction
{
    public class TransactionUpdateDto:TransactionBaseDto
    {
        public Guid TransactionId { get; set; }
    }
}
