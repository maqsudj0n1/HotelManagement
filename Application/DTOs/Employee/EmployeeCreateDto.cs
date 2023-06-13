namespace Application.DTOs.Employee;

public class EmployeeCreateDto:EmployeeBaseDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
