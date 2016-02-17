namespace Dinnerplanner.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Dish
    {
        public Dish(String name, DishType type, String image, String description) 
        {
		    Name = name;
		    Type = type;
		    Image = image;
		    Description = description;
            Ingredients = new HashSet<Ingredient>();
        }

        public string Name { get; set; }

        public DishType Type { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public HashSet<Ingredient> Ingredients { get; set; }

        public float Cost
        {
            get
            {
                return Ingredients.Sum(ingredient => (float)ingredient.Price);
            }
        }

        public bool Contains(String filter)
        {
            var lowerName = Name.ToLower();
            var lowerFilter = filter.ToLower();

            if(lowerName.Contains(lowerFilter))
                return true;

            if (Ingredients.Any(ingredient => ingredient.Name.ToLower().Contains(lowerFilter)))
                return true;
		    
            return false;
	    }
    }
}
