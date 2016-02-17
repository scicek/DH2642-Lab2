namespace Dinnerplanner.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class Dinner : IDinner
    {
	    public Dinner()
        {	
	        Dishes = new HashSet<Dish>();
	        FullMenu = new HashSet<Dish>();
	        PopulateDishes();
        }

        public int NumberOfGuests { get; set; }

        public HashSet<Dish> FullMenu { get; private set; }

        public HashSet<Ingredient> AllIngredients
        {
            get
            {
                var ingredients = new HashSet<Ingredient>();
                foreach (var ingredient in FullMenu.SelectMany(dish => dish.Ingredients))
                    ingredients.Add(ingredient);

                return ingredients;
            }
        }

        public float TotalMenuPrice
        {
            get
            {
                return NumberOfGuests * FullMenu.Sum(dish => dish.Cost);
            }
        }

        private HashSet<Dish> Dishes { get; set; }

        /// <summary>
	    /// Returns the set of dishes of specific type. (1 = starter, 2 = main, 3 = desert).
	    /// </summary>
	    /// <param name="type">The type of dish to get.</param>
	    /// <returns>All dishes of the given type.</returns>
	    public HashSet<Dish> GetDishesOfType(DishType type)
        {
		    var results = new HashSet<Dish>();

            foreach (var dish in Dishes.Where(dish => dish.Type == type))
                results.Add(dish);
		    
		    return results;
	    }

        /// <summary>
	    /// Returns the set of dishes of specific type, that contain filter in their name or name of any ingredient. 
	    /// </summary>
	    /// <param name="type">The type of dish to filter.</param>
	    /// <param name="filter">The filter.</param>
	    /// <returns>A set of dishes that match the filter.</returns>
	    public HashSet<Dish> FilterDishesOfType(DishType type, string filter)
	    {
		    var results = new HashSet<Dish>();
		    
            foreach (var dish in Dishes.Where(dish => dish.Type == type && dish.Contains(filter)))
                results.Add(dish);
            
		    return results;
	    }

        public void AddDishToMenu(Dish dish)
        {
            FullMenu.RemoveWhere(d => d.Type == dish.Type);
            FullMenu.Add(dish);
        }

        public Dish GetSelectedDish(DishType type)
        {
            return FullMenu.FirstOrDefault(dish => dish.Type == type);
        }

        public void RemoveDishFromMenu(Dish dish)
        {
            FullMenu.Remove(dish);
        }

        private void PopulateDishes()
        {
            var frenchToastDish = new Dish("French toast", DishType.Starter, "toast.jpg", "In a large mixing bowl, beat the eggs. Add the milk, brown sugar and nutmeg; stir well to combine. Soak bread slices in the egg mixture until saturated. Heat a lightly oiled griddle or frying pan over medium high heat. Brown slices on both sides, sprinkle with cinnamon and serve hot.");
            var eggsIngredient = new Ingredient("eggs", 0.5, "", 1);
            var milkIngredient = new Ingredient("milk", 30, "ml", 6);
            var brownSugarIngredient = new Ingredient("brown sugar", 7, "g", 1);
            var groundNutmegIngredient = new Ingredient("ground nutmeg", 0.5, "g", 12);
            var whiteBreadIngredient = new Ingredient("white bread", 2, "slices", 2);

            frenchToastDish.Ingredients.Add(eggsIngredient);
            frenchToastDish.Ingredients.Add(milkIngredient);
            frenchToastDish.Ingredients.Add(brownSugarIngredient);
            frenchToastDish.Ingredients.Add(groundNutmegIngredient);
            frenchToastDish.Ingredients.Add(whiteBreadIngredient);

            var meatBallsDish = new Dish("Meat balls", DishType.Main, "meatballs.jpg", "Preheat an oven to 400 degrees F (200 degrees C). Place the beef into a mixing bowl, and season with salt, onion, garlic salt, Italian seasoning, oregano, red pepper flakes, hot pepper sauce, and Worcestershire sauce; mix well. Add the milk, Parmesan cheese, and bread crumbs. Mix until evenly blended, then form into 1 1/2-inch meatballs, and place onto a baking sheet. Bake in the preheated oven until no longer pink in the center, 20 to 25 minutes.");
            var groundBeefIngredient = new Ingredient("extra lean ground beef", 115, "g", 20);
            var seaSaltIngredient = new Ingredient("sea salt", 0.7, "g", 3);
            var onionIngredient = new Ingredient("small onion, diced", 0.25, "", 2);
            var garlicSaltIngredient = new Ingredient("garlic salt", 0.6, "g", 3);
            var italianSeasoningIngredient = new Ingredient("Italian seasoning", 0.3, "g", 3);
            var driedOreganoIngredient = new Ingredient("dried oregano", 0.3, "g", 3);
            var redPepperFlakesIngredient = new Ingredient("crushed red pepper flakes", 0.6, "g", 3);
            var worcestershireSauceIngredient = new Ingredient("Worcestershire sauce", 16, "ml", 7);
            var milk2Ingredient = new Ingredient("milk", 20, "ml", 4);
            var parmesanCheeseIngredient = new Ingredient("grated Parmesan cheese", 5, "g", 8);
            var breadCrumbsIngredient = new Ingredient("seasoned bread crumbs", 115, "g", 4);

            meatBallsDish.Ingredients.Add(groundBeefIngredient);
            meatBallsDish.Ingredients.Add(seaSaltIngredient);
            meatBallsDish.Ingredients.Add(onionIngredient);
            meatBallsDish.Ingredients.Add(garlicSaltIngredient);
            meatBallsDish.Ingredients.Add(italianSeasoningIngredient);
            meatBallsDish.Ingredients.Add(driedOreganoIngredient);
            meatBallsDish.Ingredients.Add(redPepperFlakesIngredient);
            meatBallsDish.Ingredients.Add(worcestershireSauceIngredient);
            meatBallsDish.Ingredients.Add(milk2Ingredient);
            meatBallsDish.Ingredients.Add(parmesanCheeseIngredient);
            meatBallsDish.Ingredients.Add(breadCrumbsIngredient);

            var iceCream = new Dish("Ice cream", DishType.Dessert, "icecream.jpg", "Buy it from the ice cream man.");
            var milk = new Ingredient("Milk", 0.5, "liters", 5);
            iceCream.Ingredients.Add(milk);

            Dishes.Add(frenchToastDish);
            Dishes.Add(new Dish("Fake Ice Cream", DishType.Dessert, "icecream.jpg", "Buy it from the ice cream man."));
            Dishes.Add(new Dish("Fake Ice Cream", DishType.Dessert, "icecream.jpg", "Buy it from the ice cream man."));
            Dishes.Add(new Dish("Fake Ice Cream", DishType.Dessert, "icecream.jpg", "Buy it from the ice cream man."));
            Dishes.Add(new Dish("Fake Ice Cream", DishType.Dessert, "icecream.jpg", "Buy it from the ice cream man."));
            Dishes.Add(new Dish("Fake Ice Cream", DishType.Dessert, "icecream.jpg", "Buy it from the ice cream man."));
            Dishes.Add(new Dish("Fake Ice Cream", DishType.Dessert, "icecream.jpg", "Buy it from the ice cream man."));
            Dishes.Add(new Dish("Fake Ice Cream", DishType.Dessert, "icecream.jpg", "Buy it from the ice cream man."));
            Dishes.Add(new Dish("Fake Ice Cream", DishType.Dessert, "icecream.jpg", "Buy it from the ice cream man."));
            Dishes.Add(new Dish("Fake Ice Cream", DishType.Dessert, "icecream.jpg", "Buy it from the ice cream man."));
            Dishes.Add(new Dish("Fake Ice Cream", DishType.Dessert, "icecream.jpg", "Buy it from the ice cream man."));
            Dishes.Add(meatBallsDish);
            Dishes.Add(iceCream);

            AddDishToMenu(frenchToastDish);
            AddDishToMenu(meatBallsDish);
            AddDishToMenu(iceCream);

            NumberOfGuests = 2;
        }
    }
}
