namespace Application.DTOs.Payment
{
    public class PaymentCreateDto : PaymentBaseDto
    {
        public Guid CustomerId { get; set; }
    }
}
