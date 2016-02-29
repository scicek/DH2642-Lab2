namespace Dinnerplanner.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Dish
    {
        public Dish()
        {
            Ingredients = new HashSet<Ingredient>();
        }

        public Dish(String name, DishType type, String image, String description) : this()
        {
		    Name = name;
		    Type = type;
		    Image = image;
		    Description = description;
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
                return (float)Math.Round(Ingredients.Sum(ingredient => (float)ingredient.Price * ingredient.Quantity));
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

        protected bool Equals(Dish other)
        {
            return string.Equals(Name, other.Name) && Type == other.Type && string.Equals(Image, other.Image) && string.Equals(Description, other.Description) && Equals(Ingredients, other.Ingredients);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Dish) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (int) Type;
                hashCode = (hashCode*397) ^ (Image != null ? Image.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Ingredients != null ? Ingredients.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
