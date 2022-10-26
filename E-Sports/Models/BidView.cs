namespace E_Sports.Models
{
    public class BidView
    {
        public Guid Id { get; set; }
        public string PlayerName { get; set; }
        public Guid PlayerId { get; set; }
        public string TeamName { get; set; }
        public double Price { get; set; }
    }
}
