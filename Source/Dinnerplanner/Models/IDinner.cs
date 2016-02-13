namespace Dinnerplanner.Models
{
    using System.Collections.Generic;

    public interface IDinner
    {
        /// <summary>
        /// Gets the number of guests.
        /// </summary>
        int NumberOfGuests { get; set; }

        /// <summary>
        /// Returns all the dishes on the menu.
        /// </summary>
        HashSet<Dish> FullMenu { get; }

        /// <summary>
        /// Returns all ingredients for all the dishes on the menu.
        /// </summary>
        HashSet<Ingredient> AllIngredients { get; }

        /// <summary>
        /// Returns the total price of the menu (all the ingredients multiplied by number of guests).
        /// </summary>
        float TotalMenuPrice { get; }

        /// <summary>
        /// Adds a dish to the menu. If the dish of that type already exists on the menu it is removed from the menu and the new one added.
        /// </summary>
        /// <param name="dish">The dish to add.</param>
        void AddDishToMenu(Dish dish);

        /// <summary>
        /// Returns the dish that is on the menu for selected type (1 = starter, 2 = main, 3 = desert).
        /// </summary>
        /// <param name="type">The type of dish.</param>
        /// <returns>A dish.</returns>
        Dish GetSelectedDish(DishType type);

        /// <summary>
        /// Removes a dish from the menu.
        /// </summary>
        /// <param name="dish">The dish to remove.</param>
        void RemoveDishFromMenu(Dish dish);
    }
}
