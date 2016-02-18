namespace Dinnerplanner.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;
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
        
        private readonly Dinner _dinner;
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
            Mains = new ObservableCollection<Dish>();
            Desserts = new ObservableCollection<Dish>();
            Menu = new ObservableCollection<Dish>();

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

            _dinner.MenuChanged += OnMenuChanged;
            _dinner.DishesChanged += OnDishesChanged;
            _dinner.NumberOfGuestsChanged += OnNumberOfGuestsChanged;
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
                return _dinner.NumberOfGuests; 
            }

            set
            {
                _dinner.NumberOfGuests = value;
                OnPropertyChanged();
                OnPropertyChanged("TotalCost");
            }
        }

        public double TotalCost
        {
            get
            {
                return _dinner.TotalMenuPrice; 
            }
        }

        public DelegateCommand ShowPreparations { get; set; }

        public DelegateCommand ShowIngredients { get; set; }

        public void OnSelectDish(Dish dish)
        {
            _dishViewModel.Dish = dish;
            _dishViewModel.Guests = Guests;
            _dishWindow.Show();
        }

        public void OnAddDish(Dish dish)
        {
            _dinner.AddDishToMenu(dish);
        }

        public void OnRemoveDish(Dish dish)
        {
            _dinner.RemoveDishFromMenu(dish);
        }

        private void OnNumberOfGuestsChanged(object sender, EventArgs eventArgs)
        {
            OnPropertyChanged("Guests");
            OnPropertyChanged("TotalCost");
        }

        private void OnDishesChanged(object sender, EventArgs eventArgs)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Starters.Clear();
                Mains.Clear();
                Desserts.Clear();

                foreach (var dish in _dinner.GetDishesOfType(DishType.Starter))
                    Starters.Add(dish);

                foreach (var dish in _dinner.GetDishesOfType(DishType.Main))
                    Mains.Add(dish);

                foreach (var dish in _dinner.GetDishesOfType(DishType.Dessert))
                    Desserts.Add(dish);
            });
        }

        private void OnMenuChanged(object sender, EventArgs eventArgs)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Menu.Clear();
                foreach (var dish in _dinner.FullMenu)
                    Menu.Add(dish);

                ShowPreparations.RaiseCanExecute();
                ShowIngredients.RaiseCanExecute();
            });
            

            OnPropertyChanged("TotalCost");
        }
    }
}
