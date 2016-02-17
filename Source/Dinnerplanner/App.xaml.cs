namespace Dinnerplanner
{
    using System.Windows;
    using ViewModels;
    using Views;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow _mainWindow;
        private DishWindow _dishWindow;
        private IngredientsWindow _ingredientsWindow;
        private PreparationWindow _preparationWindow;

        private MainViewModel _mainViewModel;
        private DishViewModel _dishViewModel;
        private IngredientsViewModel _ingredientsViewModel;
        private PreparationViewModel _preparationViewModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _dishViewModel = new DishViewModel();
            _dishWindow = new DishWindow
            {
                DataContext = _dishViewModel
            };

            _ingredientsViewModel = new IngredientsViewModel();
            _ingredientsWindow = new IngredientsWindow
            {
                DataContext = _ingredientsViewModel
            };

            _preparationViewModel = new PreparationViewModel();
            _preparationWindow = new PreparationWindow
            {
                DataContext = _preparationViewModel
            };

            _mainViewModel = new MainViewModel(_dishWindow, _ingredientsWindow, _preparationWindow);
            _mainWindow = new MainWindow
            {
                DataContext = _mainViewModel
            };

            _mainWindow.Show();
        }
    }
}
