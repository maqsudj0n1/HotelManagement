using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities;
[Table("room")]
public class Room
{
    [Column("room_id")]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonPropertyName("room_id")]
    public Guid RoomId { get; set; }
    [Column("room_class_id")]
    public Guid RoomClassId { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Column("price")]
    public decimal Price { get; set; }
    public RoomClass RoomClass { get; set; }
    public ICollection<Reservation> Reservations { get; set; }
}
