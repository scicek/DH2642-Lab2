using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dinnerplanner.Views
{
    using System.ComponentModel;

    /// <summary>
    /// Interaction logic for DishWindow.xaml
    /// </summary>
    public partial class DishWindow : Window
    {
        public DishWindow()
        {
            InitializeComponent();
        }

        private void DishWindow_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
