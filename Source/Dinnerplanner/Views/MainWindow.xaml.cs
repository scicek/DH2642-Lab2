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

        private void OnSelectedDish(object sender, Dish dish)
        {
            _mainViewModel.OnSelectedDish(dish);
        }

        private void OnRemoveDish(object sender, RoutedEventArgs e)
        {
            _mainViewModel.OnRemoveDish((Dish)((FrameworkElement) sender).Tag);
        }
    }
}
