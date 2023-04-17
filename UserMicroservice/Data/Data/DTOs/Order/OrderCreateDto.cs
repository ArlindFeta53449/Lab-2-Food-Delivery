namespace Data.DTOs
{
    public class OrderCreateDto
    {
        public string UserId { get; set; }

        public IList<OrderItemDto> OrderItems { get; set; }

        public float Total { get; set; }
    }
}
