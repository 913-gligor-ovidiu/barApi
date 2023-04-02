using Microsoft.Build.Framework;

namespace lab3.DTOs
{
    public class OrderDTO
    {   
        public int NrOfDrinks { get; set; }

        public int ClientId { get; set; }
        public int DrinkId { get; set; }
    }
}
