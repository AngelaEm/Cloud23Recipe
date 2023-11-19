using FamEmanuelssonsRecept.Db;
using FamEmanuelssonsRecept.Models;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for KategoriWindow.xaml
    /// </summary>
    public partial class KategoriWindow : Window
    {
       
        public KategoriWindow()
        {
            InitializeComponent();
          
            CategoryListView.ItemsSource = DbHelper.LoadCategories();

            this.DataContext = this;

        }

        /// <summary>
        /// Adds a new category to database and updates category listView.
        /// </summary>
        /// <returns></returns>
        private async Task AddCategory()
        {
            try
            {
                var categoryName = CategoryTextBox.Text;
                if (!string.IsNullOrWhiteSpace(categoryName))
                {
                    Category newCategory = new Category(categoryName);

                    RecipeDbContext._DbContext._Categories.Add(newCategory);

                    await RecipeDbContext._DbContext.SaveChangesAsync();

                    CategoryListView.ItemsSource = DbHelper.LoadCategories();

                    MessageBox.Show($"Ny kategori {newCategory.Name} tillagd!");
                }
                else
                {
                    MessageBox.Show("Glöm inte att namnge din kategori!");
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Det gick inte att lägga till nu. Var god försök senare.");              
            }                    
        }

        /// <summary>
        ///  Delete category from database and updates category listView.
        /// </summary>
        /// <returns></returns>
        private async Task DeleteCategory()
        {
            var selectedCategory = CategoryListView.SelectedItem as Category;

            if (selectedCategory != null)
            {
                try
                {
                    RecipeDbContext._DbContext._Categories.Remove(selectedCategory);

                    await RecipeDbContext._DbContext.SaveChangesAsync();

                    CategoryListView.ItemsSource = DbHelper.LoadCategories();
                }
                catch (Exception)
                {
                    MessageBox.Show("Denna kategori har recept kopplade till sig. Vänligen ta bort dessa recept innan du tar bort kategorin.");
                }          
            }
        }

        /// <summary>
        /// Click event for AddCategoryBtn. Runs AddCategory Method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AddCategoryBtn_Click(object sender, RoutedEventArgs e)
        {
            await AddCategory();
        }

        /// <summary>
        /// Click event for RemoveCategoryBtn. Runs DeleteCategory Method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RemoveCategoryBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Är du säker på att du vill ta bort denna kategori?", "Bekräftelse", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                await DeleteCategory();
            }
            else
            {
                return;
            }
            
        }

        /// <summary>
        /// Click event for MainWindowBtn. Opens MainWindow and closes this window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
