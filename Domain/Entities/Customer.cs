using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("customer")]
    public class Customer
    {
        [Column("customer_id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("customer_id")]
        public Guid CustomerId { get; set; }
        [Column("full_name")]
        public string CustomerFullName { get; set; }
        [Column("address")]
        public string Address { get; set; }
        [Column("phone_number")]
        public string PhoneNumber { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

    }
}
