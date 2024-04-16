namespace DirectBookingOne.Api.Models
{
    public interface IProduct
    {
        string CategoryName { get; set; }
        int Capacity { get; set; }
        decimal PricePerNight { get; set; }
    }
}
