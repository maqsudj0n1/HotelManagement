using Domain.Entities.IdentityEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities;
[Table("employees")]
public class Employee
{
    [Column("employee_id")]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonPropertyName("employee_id")]
    public Guid EmployeeId { get; set; }
    [Column("fuulname")]
    public string FullName { get; set; }
    [Column("address")]
    public string Address { get; set; }
    [Column("phone_number")]
    public string PhoneNumber { get; set; }
    [Column("username")]
    public string Username { get; set; }
    [Column("password")]
    public string Password { get; set; }
    public virtual ICollection<Role>? Roles { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}
