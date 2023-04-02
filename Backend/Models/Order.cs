namespace lab3.Models
{
    public class Order
    {  
       
        public DateTime DateTime { get; set; }
        public int NrOfDrinks { get; set; }

        public int ClientId { get; set; }
        public int DrinkId { get; set; }
        
        //Hidden from the API (not exposed to the client)
        public Client? Client { get; set; } = null!;
        public Drink? Drink { get; set; } = null!;
    }
}
