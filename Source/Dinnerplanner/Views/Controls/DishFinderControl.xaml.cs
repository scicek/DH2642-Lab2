namespace Dinnerplanner.Views.Controls
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Annotations;
    using Models;

    /// <summary>
    /// Interaction logic for DishFinderControl.xaml
    /// </summary>
    public partial class DishFinderControl : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty DishesProperty = DependencyProperty.Register("Dishes",
            typeof (ObservableCollection<Dish>), typeof (DishFinderControl),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public DishFinderControl()
        {
            InitializeComponent();
        }

        public event EventHandler<Dish> SelectDish;
        public event EventHandler<Dish> AddDish;

        public ObservableCollection<Dish> Dishes
        {
            get { return (ObservableCollection<Dish>) GetValue(DishesProperty); }

            set
            {
                SetValue(DishesProperty, value);
                OnPropertyChanged();
            }
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SelectDish.Raise(this, (Dish) ((FrameworkElement) sender).Tag);
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            AddDish.Raise(this, (Dish)((FrameworkElement)sender).Tag);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
