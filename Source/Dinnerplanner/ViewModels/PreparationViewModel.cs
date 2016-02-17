namespace Dinnerplanner.ViewModels
{
    using Models;

    public class PreparationViewModel : NotifyPropertyChangedBase
    {
        private Dish _starter;
        private Dish _main;
        private Dish _dessert;

        public PreparationViewModel()
        {
            Starter = new Dish("lol", DishType.Starter, "meatballs.jpg", "ok");
            Starter.Ingredients.Add(new Ingredient("shit", 200, "oz", 1.4));

            Main = new Dish("lol", DishType.Main, "meatballs.jpg", "ok");
            Main.Ingredients.Add(new Ingredient("shit", 200, "oz", 1.4));

            Dessert = new Dish("lol", DishType.Dessert, "meatballs.jpg", "ok");
            Dessert.Ingredients.Add(new Ingredient("shit", 200, "oz", 1.4));
        }

        public Dish Starter
        {
            get
            {
                return _starter; 
                
            }

            set
            {
                _starter = value;
                OnPropertyChanged();
            }
        }

        public Dish Main
        {
            get
            {
                return _main; 
                
            }

            set
            {
                _main = value;
                OnPropertyChanged();
            }
        }

        public Dish Dessert
        {
            get
            {
                return _dessert;
                
            }

            set
            {
                _dessert = value;
                OnPropertyChanged();
            }
        }
    }
}
