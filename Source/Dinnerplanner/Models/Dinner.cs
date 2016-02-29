namespace Dinnerplanner.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Windows;
    using BigOven;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class Dinner : IDinner
    {
        private const string BigOvenURL = "http://api.bigoven.com/";
        private const string BigOvenApiKey = "18f3cT02U9f6yRl3OKDpP8NA537kxYKu";
        private int _numberOfGuests;
        private HashSet<Dish> _fullMenu;
        private HashSet<Dish> _dishes;

        public Dinner()
        {	
	        Dishes = new HashSet<Dish>();
	        FullMenu = new HashSet<Dish>();
        }

        public event EventHandler DishesChanged; 
        public event EventHandler MenuChanged; 
        public event EventHandler NumberOfGuestsChanged;
        public event EventHandler<Tuple<DishType, HashSet<Dish>>> FilteredDishes;

        public int NumberOfGuests
        {
            get
            {
                return _numberOfGuests; 
            }

            set
            {
                _numberOfGuests = value;
                NumberOfGuestsChanged.Raise(this);
            }
        }

        public HashSet<Dish> FullMenu
        {
            get
            {
                return _fullMenu;
            }

            private set
            {
                _fullMenu = value;
                MenuChanged.Raise(this);
            }
        }

        public void GetAllDishes(DishType type, string filter)
        {
            Task.Run(() =>
            {
                var result = GetRecipes(type, filter);
                FilteredDishes.Raise(this, new Tuple<DishType, HashSet<Dish>>(type, result.Result));
            }); 
        }

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
                return (float) Math.Round(NumberOfGuests * FullMenu.Sum(dish => dish.Cost));
            }
        }

        private HashSet<Dish> Dishes
        {
            get
            {
                return _dishes; 
            }

            set
            {
                _dishes = value;
                DishesChanged.Raise(this);
            }
        }

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
            MenuChanged.Raise(this);
        }

        public Dish GetSelectedDish(DishType type)
        {
            return FullMenu.FirstOrDefault(dish => dish.Type == type);
        }

        public void RemoveDishFromMenu(Dish dish)
        {
            FullMenu.Remove(dish);
            MenuChanged.Raise(this);
        }

        private void AddDish(Dish dish)
        {
            Dishes.Add(dish);
            DishesChanged.Raise(this);
        }

        private static async Task<HashSet<Dish>> GetRecipes(DishType type, string filter)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BigOvenURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync("recipes?api_key=" + BigOvenApiKey + "&any_kw=" + filter + "&pg=1&rpp=10").ConfigureAwait(false);
                    var set = new HashSet<Dish>();
                    
                    if (response.IsSuccessStatusCode)
                    {
                        var recipes = await response.Content.ReadAsAsync<object>();
                        dynamic parsedRecipes = JObject.Parse(recipes.ToString());

                        foreach (var result in parsedRecipes.Results)
                        {
                            try
                            {
                                response = await client.GetAsync("recipe/" + result.RecipeID + "/?api_key=" + BigOvenApiKey).ConfigureAwait(false);
                                var recipe = await response.Content.ReadAsAsync<BigOvenRecipe>();

                                if (ConvertRecipeCategoryToDishType(recipe.Category) != type)
                                    continue;

                                var dish = CreateDishFromRecipe(recipe);
                                if  (dish != null)
                                    set.Add(dish);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                    }

                    return set;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        private static DishType ConvertRecipeCategoryToDishType(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return DishType.None;

            if (category.ToLowerInvariant() == "appetizers")
                return DishType.Starter;
            if (category.ToLowerInvariant() == "main dish")
                return DishType.Main;
            if (category.ToLowerInvariant() == "desserts")
                return DishType.Dessert;

            return DishType.None;
        }

        private static string ConvertDishTypeToRecipeCategory(DishType type)
        {
            if (type == DishType.None)
                return null;

            if (type == DishType.Starter)
                return "appetizers";
            if (type == DishType.Main)
                return "main dish";
            if (type == DishType.Dessert)
                return "desserts";

            return null;
        }

        private static Dish CreateDishFromRecipe(BigOvenRecipe recipe)
        {
            if (recipe == null)
                return null;

            var dish = new Dish
            {
                Name = recipe.Title,
                Type = ConvertRecipeCategoryToDishType(recipe.Category),
                Image = recipe.ImageURL,
                Description = (!string.IsNullOrWhiteSpace(recipe.Description) ? recipe.Description + "\n\n" : string.Empty) + recipe.Instructions
            };

            if (recipe.Ingredients != null && recipe.Ingredients.Any())
                foreach (var ingredient in recipe.Ingredients)
                    dish.Ingredients.Add(new Ingredient(ingredient.Name, ingredient.Quantity ?? 0, ingredient.Unit, 1));

            if (dish.Name == null || dish.Image == null || dish.Type == DishType.None || !dish.Ingredients.Any())
                return null;

            return dish;
        }
    }
}
