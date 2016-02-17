namespace Dinnerplanner.Views
{
    using System.ComponentModel;
    using System.Windows;

    /// <summary>
    /// Interaction logic for IngredientsWindow.xaml
    /// </summary>
    public partial class IngredientsWindow : Window
    {
        public IngredientsWindow()
        {
            InitializeComponent();
        }

        private void IngredientsWindow_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
