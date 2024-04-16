namespace DirectBookingOne.Api.DataAccess
{
    public class AvailabilityDataBaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string AvailabilityCollectionName { get; set; } = null!;
    }
}
