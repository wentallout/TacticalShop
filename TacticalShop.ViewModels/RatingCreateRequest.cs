namespace TacticalShop.ViewModels
{
    public class RatingCreateRequest
    {
        public string UserId { get; set; }

        public int ProductId { get; set; }

        public int Star { get; set; }
    }
}
