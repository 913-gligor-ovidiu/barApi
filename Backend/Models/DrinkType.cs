namespace lab3.Models
{
    public class DrinkType
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int NrOfBrands { get; set; }
        public int Stock { get; set; } //how many boxes of this type are in stock
        public float ProfitMargin { get; set; }

        //Hidden from the API (not exposed to the client)
        public virtual ICollection<Drink> Drinks { get; set; } = null!;
    }
}
