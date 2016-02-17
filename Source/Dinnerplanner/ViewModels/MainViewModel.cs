namespace Dinnerplanner.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Models;
    using Views;

    public class MainViewModel : NotifyPropertyChangedBase
    {
        private readonly DishWindow _dishWindow;
        private readonly IngredientsWindow _ingredientsWindow;
        private readonly PreparationWindow _preparationWindow;

        private readonly DishViewModel _dishViewModel;
        private readonly IngredientsViewModel _ingredientsViewModel;
        private readonly PreparationViewModel _preparationViewModel;
        
        private Dinner _dinner;
        private int _guests;
        private double _totalCost;
        private ObservableCollection<Dish> _starters;
        private ObservableCollection<Dish> _mains;
        private ObservableCollection<Dish> _desserts;

        public MainViewModel(DishWindow dishWindow, IngredientsWindow ingredientsWindow, PreparationWindow preparationWindow)
        {
            _dinner = new Dinner();
            _dishWindow = dishWindow;
            _ingredientsWindow = ingredientsWindow;
            _preparationWindow = preparationWindow;

            _dishViewModel = (DishViewModel) _dishWindow.DataContext;
            _ingredientsViewModel = (IngredientsViewModel) _ingredientsWindow.DataContext;
            _preparationViewModel = (PreparationViewModel) _preparationWindow.DataContext;
           
            Starters = new ObservableCollection<Dish>();
            _mains = new ObservableCollection<Dish>();
            Desserts = new ObservableCollection<Dish>();
            Menu = new ObservableCollection<Dish>();
            Guests = _dinner.NumberOfGuests;
            TotalCost = _dinner.TotalMenuPrice;
            ShowPreparations = new DelegateCommand(o =>
            {
                _preparationViewModel.Starter = _dinner.GetSelectedDish(DishType.Starter);
                _preparationViewModel.Main = _dinner.GetSelectedDish(DishType.Main);
                _preparationViewModel.Dessert = _dinner.GetSelectedDish(DishType.Dessert);
                _preparationWindow.Show();
            }, o => Menu.Any());
            ShowIngredients = new DelegateCommand(o =>
            {
                _ingredientsViewModel.Ingredients = _dinner.AllIngredients;
                _ingredientsWindow.Show();
            }, o => Menu.Any());

            foreach (var dish in _dinner.GetDishesOfType(DishType.Starter))
                Starters.Add(dish);

            foreach (var dish in _dinner.GetDishesOfType(DishType.Main))
                Mains.Add(dish);

            foreach (var dish in _dinner.GetDishesOfType(DishType.Dessert))
                Desserts.Add(dish);

            foreach (var dish in _dinner.FullMenu)
            {
                Menu.Add(dish);
            }
        }

        public ObservableCollection<Dish> Starters
        {
            get
            {
                return _starters;
            }
            
            set
            {
                _starters = value; 
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Dish> Mains
        {
            get
            {
                return _mains; 
                
            }

            set
            {
                _mains = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Dish> Desserts
        {
            get
            {
                return _desserts; 
                
            }

            set
            {
                _desserts = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Dish> Menu { get; private set; }

        public int Guests
        {
            get
            {
                return _guests; 
                
            }

            set
            {
                _guests = value;
                _dinner.NumberOfGuests = _guests;
                TotalCost = _dinner.TotalMenuPrice;
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

        public ICommand ShowPreparations { get; set; }

        public ICommand ShowIngredients { get; set; }

        public void OnSelectedDish(Dish dish)
        {
            _dishViewModel.Dish = dish;
            _dishViewModel.Guests = Guests;
            _dishWindow.Show();
        }

        public void OnRemoveDish(Dish dish)
        {
            Menu.Remove(dish);
            _dinner.RemoveDishFromMenu(dish);
            TotalCost = _dinner.TotalMenuPrice;
        }
    }
}
