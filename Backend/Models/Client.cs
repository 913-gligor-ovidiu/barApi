namespace lab3.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }

        //Hidden from the API (not exposed to the client)
        public virtual ICollection<Order> Orders { get; set; } = null!;
    }
}
