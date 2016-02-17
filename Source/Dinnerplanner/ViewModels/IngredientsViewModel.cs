namespace Dinnerplanner.ViewModels
{
    using System.Collections.Generic;
    using Models;

    public class IngredientsViewModel : NotifyPropertyChangedBase
    {
        private HashSet<Ingredient> _ingredients;

        public HashSet<Ingredient> Ingredients
        {
            get
            {
                return _ingredients;
            }

            set
            {
                _ingredients = value;
                OnPropertyChanged();
            }
        }
    }
}
