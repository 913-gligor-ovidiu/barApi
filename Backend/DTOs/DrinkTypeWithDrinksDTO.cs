using System.Collections.ObjectModel;

namespace lab3.DTOs
{
    public class DrinkTypeWithDrinksDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int NrOfBrands { get; set; }
        public int Stock { get; set; } //how many boxes of this type are in stock
        public float ProfitMargin { get; set; }
        public List<DrinkDTO>? Drinks { get; set; }
    }
}
