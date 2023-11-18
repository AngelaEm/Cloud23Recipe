using FamEmanuelssonsRecept.Models;
using FamEmanuelssonsRecept.Views;
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

namespace FamEmanuelssonsRecept.Windows
{
    /// <summary>
    /// Interaction logic for ReceptWindow.xaml
    /// </summary>
    public partial class ReceptWindow : Window
    {
        
        public ReceptWindow()
        {
            InitializeComponent();

            this.DataContext = DbHelper.SelectedRecipe;
        }

        /// <summary>
        /// Click event for MainWindowBtn. Opens mainWindow and closes this window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        /// <summary>
        /// Click event for EditWindowBtn. Opens EditWindow and closes this window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow();
            editWindow.Show();
            this.Close();
        }
    }
}
