namespace Application.DTOs.Employee;

public class EmployeeBaseDto
{
    public string FullName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public Guid[] Roles { get; set; }
}
