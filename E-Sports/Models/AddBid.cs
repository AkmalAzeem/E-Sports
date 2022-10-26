namespace E_Sports.Models
{
    public class AddBid
    {
        public Guid Id { get; set; }
        public string PlayerName { get; set; }
        public Guid PlayerId { get; set; }
        public string TeamName { get; set; }
        public Guid TeamId { get; set; }
        public double Price { get; set; }

        public double CanSpend { get; set; }
    }
}
