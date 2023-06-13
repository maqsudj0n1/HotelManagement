using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities;

[Table("reservation")]
public class Reservation
{
    [Column("reservation_id")]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonPropertyName("reservation_id")]
    public Guid ReservationId { get; set; }
    [Column("customer_id")]
    public Guid CustomerId { get; set; }
    [Column("room_id")]
    public Guid RoomId { get; set; }
    [Column("reservation_date")]
    public DateTime ReservationDate { get; set; }
    [Column("date_in")]
    public DateTime DateIn { get; set; }
    [Column("date_out")]
    public DateTime DateOut { get; set; }
    [Column("days_range")]
    public int DaysRange { get; set; }
    public Customer Customer { get; set; }
    public Room Room { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}
