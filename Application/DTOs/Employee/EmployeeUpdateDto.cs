using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Employee
{
    public class EmployeeUpdateDto:EmployeeBaseDto
    {
        public Guid EmployeeId { get; set; }
    }
}
