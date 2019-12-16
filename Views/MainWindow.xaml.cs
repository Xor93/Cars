using Cars.ViewModel;
using System;
using System.Windows;

namespace Cars.Views
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            ViewModelLocator.Cleanup();
        }
    }
}
