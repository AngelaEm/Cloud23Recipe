using FamEmanuelssonsRecept.Db;
using FamEmanuelssonsRecept.Helpers;
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

        /// <summary>
        /// Updates selected recipe information with the values entered by user and saves the changes to the database.
        /// </summary>
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
                MessageBox.Show("Ogiltigt betyg angivet. Betyget har satts till 0."); 
                DbHelper.SelectedRecipe.Grade = 0;
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

        /// <summary>
        /// Delete the selected recipe and save changes to database. Opens mainWindow and closes this window.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Click event for EditRecipeWindow. Runs EditRecipe method and sets selectedRecipe and selectedCategory to null. Open mainWindow and closes this window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void EditRecipeBtn_Click(object sender, RoutedEventArgs e)
        {
            await EditRecipe();

            DbHelper.SelectedRecipe = null;
            DbHelper.SelectedCategory = null;

            MessageBox.Show("Receptet är ändrat!");

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();

        }

        /// <summary>
        /// Click event for DeleteRecipeBtn. Shows a warning att let user pres yes or no. If yes the DeleteRecipe Method runs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Click event for Button. Opens MainWindow and closes this window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
