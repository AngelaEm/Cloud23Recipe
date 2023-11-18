using FamEmanuelssonsRecept.Db;
using FamEmanuelssonsRecept.Models;
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

namespace FamEmanuelssonsRecept.Views
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public EditWindow()
        {
            InitializeComponent();

            CategoryComboBox.ItemsSource = DbHelper.LoadCategories();
           
            this.DataContext = DbHelper.SelectedRecipe;

            
        }

       
        private async Task EditRecipe()
        {
            DbHelper.SelectedRecipe.Name = RecipeNameTextBox.Text;
            DbHelper.SelectedRecipe.Ingredients = IngredientsTextBox.Text;
            DbHelper.SelectedRecipe.Description = DescriptionTextBox.Text;
            
            var gradeTextBox = GradeTextBox.Text;

            double grade;

            if (double.TryParse(gradeTextBox, out grade))
            {
                DbHelper.SelectedRecipe.Grade = grade;
            }
            else
            {
                DbHelper.SelectedRecipe.Grade = 10;
            }

            var imagePath = ImageTextBox.Text;

            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                DbHelper.SelectedRecipe.ImagePath = imagePath;
            }
            else
            {

                DbHelper.SelectedRecipe.ImagePath = "/Images/middag.png";
            }

            var category = CategoryComboBox.SelectedItem as Category;

            if (category != null)
            {
                DbHelper.SelectedRecipe.Category = category;
            }

            
            await RecipeDbContext._DbContext.SaveChangesAsync();

        }

        private async void EditRecipeBtn_Click(object sender, RoutedEventArgs e)
        {
            await EditRecipe();

            DbHelper.SelectedRecipe = null;
            DbHelper.SelectedCategory = null;

            MessageBox.Show("Successfully changed!");

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();

        }

        private async Task DeleteRecipe()
        {
           
            if (DbHelper.SelectedRecipe != null)
            {
                RecipeDbContext._DbContext._Recipes.Remove(DbHelper.SelectedRecipe);
                await RecipeDbContext._DbContext.SaveChangesAsync();              
            }

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();

        }

        private async void DeleteRecipeBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Är du säker på att du vill ta bort detta recept?", "Bekräftelse", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                await DeleteRecipe();
            }
            else
            {
                return;
            }

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }



    }
}
