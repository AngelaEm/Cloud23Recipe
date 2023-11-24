using FamEmanuelssonsRecept.Db;
using FamEmanuelssonsRecept.Helpers;
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

        /// <summary>
        /// Adds a new recipe to the database with the recipe name, ingredients, description, grade, image path, and category. 
        /// </summary>
        private async Task AddRecipe()
        {
            
            var recipeName = RecipeNameTextBox.Text;                                 
            var ingredients = IngredientsTextBox.Text;
            var description = DescriptionTextBox.Text;
            var gradeTextBox = GradeTextBox.Text;

            double grade;

            if (double.TryParse(gradeTextBox, out grade))
            {
                DbHelper.SelectedRecipe.Grade = grade;
            }
            else
            {
                MessageBox.Show("Ogiltigt betyg angivet. Betyget har satts till 0!");
                                
            }

            if (string.IsNullOrWhiteSpace(recipeName) || string.IsNullOrWhiteSpace(ingredients) || string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Obs, fyll i alla fält!");
                return;
            }

            Recipe newRecipe = new Recipe(recipeName,ingredients, description, grade);
            
            var imagePath = ImageTextBox.Text;

            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                newRecipe.ImagePath = SetRecipeImage(imagePath);
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

        /// <summary>
        /// Sets question image based on image path
        /// </summary>
        /// <param name="imagePath">The path to the image file</param>
        private string SetRecipeImage(string imagePath)
        {
            try
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);

                if (imagePath.StartsWith("http://") || imagePath.StartsWith("https://"))
                {
                    image.CacheOption = BitmapCacheOption.OnLoad;
                }

                image.EndInit();
                return imagePath;
            }
            
            catch (Exception ex)
            {
                MessageBox.Show($"Det gick inte att ladda bilden: {ex.Message}");
                return null;
            }

        }      

        /// <summary>
        /// Click event for Button with a reminder to add recipe. Opens MainWindow and closes this window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Är du säker på att du vill gå tillbaka? Har du lagt till receptet?", "Bekräftelse", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                return;
            }
           
        }

        /// <summary>
        /// Click event for AddRecipeBtn. Runs AddRecipeMethod, shows a message about recipe being added, opens MainWindow and closes this window. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AddRecipeBtn_Click(object sender, RoutedEventArgs e)
        {
            await AddRecipe();
            MessageBox.Show($"Recept {RecipeNameTextBox.Text} tillagt!");
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
