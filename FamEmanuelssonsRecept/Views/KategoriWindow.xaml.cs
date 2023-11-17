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

        private void MainWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();


        }

        private async Task AddCategory()
        {
            

            var categoryName = CategoryTextBox.Text;
            Category newCategory = new Category(categoryName);
            RecipeDbContext._DbContext._Categories.Add(newCategory);
            await RecipeDbContext._DbContext.SaveChangesAsync();
            CategoryListView.ItemsSource = DbHelper.LoadCategories();
           
        }

        private async void AddCategoryBtn_Click(object sender, RoutedEventArgs e)
        {
            await AddCategory();
        }

        private async Task DeleteCategory()
        {
            var selectedCategory = CategoryListView.SelectedItem as Category;

            if (selectedCategory != null)
            {
                RecipeDbContext._DbContext._Categories.Remove(selectedCategory);
                await RecipeDbContext._DbContext.SaveChangesAsync();
               
                CategoryListView.ItemsSource = DbHelper.LoadCategories();
               
            }
        }

        private async void RemoveCategoryBtn_Click(object sender, RoutedEventArgs e)
        {
            await DeleteCategory();
        }

        
    }
}
