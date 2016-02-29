namespace Dinnerplanner.Models
{
    using System;
    using System.Collections.Generic;

    public interface IDinner
    {
        /// <summary>
        /// Fired after a call to <see cref="GetAllDishes"/>.
        /// </summary>
        event EventHandler<Tuple<DishType, HashSet<Dish>>> FilteredDishes; 

        /// <summary>
        /// Gets the number of guests.
        /// </summary>
        int NumberOfGuests { get; set; }

        /// <summary>
        /// Returns all the dishes on the menu.
        /// </summary>
        HashSet<Dish> FullMenu { get; }

        /// <summary>
        /// Gets all dishes that match the filter. The dishes are retrieved asynchronously so use <see cref="FilteredDishes"/> to get the dishes.
        /// </summary>
        /// <param name="type">The type of dishes to get.</param>
        /// <param name="filter">Used to filter the dishes.</param>
        void GetAllDishes(DishType type, string filter);

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
