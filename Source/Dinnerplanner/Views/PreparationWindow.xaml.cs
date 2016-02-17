namespace Dinnerplanner.Views
{
    using System.ComponentModel;
    using System.Windows;

    /// <summary>
    /// Interaction logic for PreparationWindow.xaml
    /// </summary>
    public partial class PreparationWindow : Window
    {
        public PreparationWindow()
        {
            InitializeComponent();
        }

        private void PreparationWindow_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
