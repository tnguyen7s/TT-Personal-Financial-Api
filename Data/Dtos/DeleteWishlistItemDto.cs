namespace Personal_Financial_WebApi.Data.Dtos
{
    public class DeleteWishlistItemDto
    {
        public string? Item { get; set; }

        public bool Purchased { get; set; }

        public DateTime Date { get; set; }
    }
}