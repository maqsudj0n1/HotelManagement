using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities;
[Table("roomClass")]

public class RoomClass
{
    [Column("id")]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonPropertyName("id")]
    public Guid RoomClassId { get; set; }
    [Column("name")]
    public string Name { get; set; }
    public ICollection<Room> Rooms { get; set; }
}
