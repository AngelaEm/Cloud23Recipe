using FamEmanuelssonsRecept.Db;
using FamEmanuelssonsRecept.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for AddReceptWindow.xaml
    /// </summary>
    public partial class AddReceptWindow : Window
    {
        public AddReceptWindow()
        {
            InitializeComponent();

            CategoryComboBox.ItemsSource = DbHelper.LoadCategories();

            this.DataContext = this;
        }

        private async Task AddRecipe()
        {
            
            var recipeName = RecipeNameTextBox.Text;                                 
            var ingredients = IngredientsTextBox.Text;
            var description = DescriptionTextBox.Text;
            var gradeTextBox = GradeTextBox.Text;

            double grade;

            if (!double.TryParse(gradeTextBox, out grade))
            {
                grade = 10;
            }
           
            Recipe newRecipe = new Recipe(recipeName,ingredients, description, grade);
            
            var imagePath = ImageTextBox.Text;

            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                newRecipe.ImagePath = imagePath;
            }
            else
            {
                
                newRecipe.ImagePath = "/Images/middag.png";
            }

            var category = CategoryComboBox.SelectedItem as Category;

            if (category != null)
            {
                newRecipe.Category = category;
            }
            else
            {
                MessageBox.Show("Obs, välj kategori!");
                return;
            }

            RecipeDbContext._DbContext._Recipes.Add(newRecipe);

            await RecipeDbContext._DbContext.SaveChangesAsync();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private async void AddRecipeBtn_Click(object sender, RoutedEventArgs e)
        {
            await AddRecipe();
            MessageBox.Show("Successfully added!");
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
