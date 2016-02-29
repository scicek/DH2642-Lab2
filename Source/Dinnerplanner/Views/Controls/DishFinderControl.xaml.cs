namespace Dinnerplanner.Views.Controls
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
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
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, DishesChanged));

        public DishFinderControl()
        {
            InitializeComponent();
        }

        public event EventHandler<Dish> SelectDish;
        public event EventHandler<Dish> AddDish;
        public event EventHandler<string> Search;

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
        
        private void SearchTextBox_OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Search.Raise(this, SearchTextBox.Text);
                Loading.Visibility = Visibility.Visible;
            }
        }

        private static void DishesChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var control = (DishFinderControl) dependencyObject;
            if (control == null || control.DishesList.Items == null)
                return;

            var collection = (INotifyCollectionChanged)control.DishesList.Items;
            collection.CollectionChanged += (sender, args) => control.Loading.Visibility = Visibility.Hidden;
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
