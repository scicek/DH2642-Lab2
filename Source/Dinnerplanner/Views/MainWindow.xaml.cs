namespace Dinnerplanner.Views
{
    using System.Windows;
    using Models;
    using ViewModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _mainViewModel;

        public MainWindow()
        {
            InitializeComponent();
            
            Loaded += (sender, args) => _mainViewModel = (MainViewModel) DataContext;
        }

        private void OnSelectDish(object sender, Dish dish)
        {
            _mainViewModel.OnSelectDish(dish);
        }

        private void OnAddDish(object sender, Dish dish)
        {
            _mainViewModel.OnAddDish(dish);
        }

        private void OnRemoveDish(object sender, RoutedEventArgs e)
        {
            _mainViewModel.OnRemoveDish((Dish)((FrameworkElement) sender).Tag);
        }

        private void StarterOnSearch(object sender, string e)
        {
            _mainViewModel.FilterDishes(DishType.Starter, e);
        }

        private void MainOnSearch(object sender, string e)
        {
            _mainViewModel.FilterDishes(DishType.Main, e);
        }

        private void DessertOnSearch(object sender, string e)
        {
            _mainViewModel.FilterDishes(DishType.Dessert, e);
        }
    }
}
