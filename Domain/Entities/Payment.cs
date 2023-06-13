using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    [Table("payment")]
    public class Payment
    {
        [Column("payment_id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("payment_id")]
        public Guid PaymentId { get; set; }
        [Column("customer_id")]
        public Guid CustomerId { get; set; }
        [Column("payment_type")]
        public string Type { get; set; }
        [Column("payment_date")]
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public ICollection<Transaction> Transactions { get; set; }
        public Customer Customer { get; set; }
    }
}
