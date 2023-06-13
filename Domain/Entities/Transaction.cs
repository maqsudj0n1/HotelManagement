using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities;
[Table("transaction")]
public class Transaction
{
    [Column("transaction_id")]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonPropertyName("transaction_id")]
    public Guid TransactionId { get; set; }
    [Column("transaction_name")]
    public string TransactionName { get; set; }
    [Column("customer_id")]
    public Guid CustomerId { get; set; }
    [Column("employee_id")]
    public Guid EmployeeId { get; set; }
    [Column("reservation_id")]
    public Guid ReservationId { get; set; }
    [Column("transaction_date")]
    public DateTime TransactionDate { get; set; }
    public Customer Customer { get; set; }
    public Payment Payment { get; set; }
    public Employee Employee { get; set; }
    public Reservation Reservation { get; set; }
}
