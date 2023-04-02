namespace lab3.DTOs
{
    public class DrinkDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
        public float? Abv { get; set; }
        public int? DrinkTypeId { get; set; }
    }
}
