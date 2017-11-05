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
using TransportLines.View;

namespace TransportLines
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new TransportVM();
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            SearchResult sr = new SearchResult();
            sr.Show();
        }

        private void hupReg_Click(object sender, RoutedEventArgs e)
        {
            Login lg = new Login();
            lg.Show();
        }
    }
}
