namespace Dinnerplanner.Views.Controls
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using Annotations;
    using Models;

    /// <summary>
    /// Interaction logic for IngredientsGridControl.xaml
    /// </summary>
    public partial class IngredientsGridControl : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty IngredientsProperty = DependencyProperty.Register("Ingredients", typeof(HashSet<Ingredient>), typeof(IngredientsGridControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public IngredientsGridControl()
        {
            InitializeComponent();
        }

        public HashSet<Ingredient> Ingredients
        {
            get
            {
                return (HashSet<Ingredient>)GetValue(IngredientsProperty);
            }

            set
            {
                SetValue(IngredientsProperty, value);
                OnPropertyChanged();
            }
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
