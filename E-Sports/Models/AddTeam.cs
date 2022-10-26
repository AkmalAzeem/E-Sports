namespace E_Sports.Models
{
    public class AddTeam
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Trophy { get; set; }
        public double SpendLimit { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
