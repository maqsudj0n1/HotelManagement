using System.ComponentModel.DataAnnotations.Schema;

namespace Application.DTOs.Customer;

public class CustomerBaseDto
{
    public string CustomerFullName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
}
