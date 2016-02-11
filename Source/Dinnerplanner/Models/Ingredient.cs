namespace Dinnerplanner.Models
{
    public class Ingredient
    {
        public Ingredient(string name, double quantity, string unit, double price)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Price = price;
        }

        public string Name { get; set; }

        // Can be empty, for example in case of eggs (there is not unit)
        public string Unit{ get; set; }
        
        public double Quantity { get; set; }
        
        public double Price { get; set; }
    }
}
