using System.ComponentModel.DataAnnotations.Schema;

namespace Application.DTOs.Reservation
{
    public class ReservationBaseDto
    {
        public DateTime ReservationDate { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }
        public int DaysRange { get; set; }
    }
}
