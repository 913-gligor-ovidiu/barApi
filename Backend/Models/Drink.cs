namespace lab3.Models
{
    public class Drink
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
        public float? Abv { get; set; }
        public int? DrinkTypeId { get; set; }

        //Hidden from the API (not exposed to the client)

        public DrinkType DrinkType { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; } = null!;

    }
}
