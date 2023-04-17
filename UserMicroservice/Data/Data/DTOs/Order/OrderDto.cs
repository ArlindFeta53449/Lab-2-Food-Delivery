namespace Data.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public string UserId { get; set; }

        public IList<OrderItemDto> OrderItems { get; set; }

        public float Total { get; set; }
    }
}
