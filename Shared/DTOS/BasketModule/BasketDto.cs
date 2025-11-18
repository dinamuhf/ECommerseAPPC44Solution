namespace Shared.Dtos.BasketModule
{
    public class BasketDto
    {
        public string Id { get; set; } = string.Empty;
        public ICollection<BasketItemDto> Items { get; set; } = [];
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public decimal? ShippingPrice { get; set; }//DeliveryMethod.Price
        public int? DeliveryMethodId { get; set; }
    }
}
