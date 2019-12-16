using Cars.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Cars.Views.UserControls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Authentication : UserControl
    {
        ObservableCollection<User> Users = new ObservableCollection<User>();
        public Authentication()
        {
            InitializeComponent();
        }
    }
}
