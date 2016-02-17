namespace Dinnerplanner.ViewModels
{
    using Models;

    public class DishViewModel : NotifyPropertyChangedBase
    {
        private Dish _dish;
        private string _title;
        private int _guests;
        private double _totalCost;

        public DishViewModel()
        {
            Title = "Dinner planner";
        }

        public string Title
        {
            get
            {
                return _title;
            }
            
            set
            {
                _title = value; 
                OnPropertyChanged(); 
            }
        }

        public int Guests
        {
            get
            {
                return _guests; 
                
            }

            set
            {
                _guests = value;

                if (_guests <= 0 || Dish == null)
                    TotalCost = 0;
                else
                    TotalCost = Dish.Cost * _guests;
                
                OnPropertyChanged();
            }
        }

        public Dish Dish
        {
            get
            {
                return _dish;
            }

            set
            {
                _dish = value;

                if (_dish != null)
                {
                    Title = "Dinner planner - " + _dish.Name;
                    TotalCost = _dish.Cost*Guests;
                }
                OnPropertyChanged();
            }
        }

        public double TotalCost
        {
            get
            {
                return _totalCost; 
                
            }

            set
            {
                _totalCost = value; 
                OnPropertyChanged();
            }
        }
    }
}
