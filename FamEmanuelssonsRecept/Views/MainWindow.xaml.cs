using FamEmanuelssonsRecept.Db;
using FamEmanuelssonsRecept.Models;
using FamEmanuelssonsRecept.Views;
using FamEmanuelssonsRecept.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FamEmanuelssonsRecept
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();

            KategoriListView.ItemsSource = DbHelper.LoadCategories();
            ReceptListView.ItemsSource = DbHelper.LoadRecipes();

        }

        /// <summary>
        /// Selects a random recipe from the database, sets it as the selected recipe, and displays it in a RecipeWindow.
        /// </summary>
        private void RandomRecipe()
        {
            Random random = new Random();
            List<Recipe> recipes = DbHelper.LoadRecipes();
            var randomIndex = random.Next(0, recipes.Count);

            try
            {
            Recipe randomRecipe = recipes.FirstOrDefault(r => r.Id == recipes[randomIndex].Id);

            DbHelper.SelectedRecipe = randomRecipe;

            ReceptWindow receptWindow = new ReceptWindow();
            receptWindow.Show();
            this.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Något gick fel, testa igen!");
            }
        }

        /// <summary>
        /// Searches for recipes containing a specified searchWord and displays matching recipes in listView
        /// </summary>
        private void SearchRecipe()
        {
            var searchWord = SearchRecipeTextBox.Text;
            try
            {
                var searchResult = RecipeDbContext._DbContext._Recipes.ToList().FindAll(r => r.Ingredients.ToLower().Contains(searchWord.ToLower()));
                if (searchResult.Count > 0)
                {
                    ReceptListView.ItemsSource = searchResult.ToList();
                }
                else
                {
                    MessageBox.Show("Hittade inga matchande recept tyvärr. Testa annat sökord!");
                }         
            }
            catch (Exception e)
            {
               MessageBox.Show(e.Message);          
            }
        }

        /// <summary>
        /// SelectionChangesEvent for KategoriListView. Sets selectedCategory and shows the recipes with the selected category.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KategoriListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DbHelper.SelectedCategory = KategoriListView.SelectedItem as Category;

            if (DbHelper.SelectedCategory != null)
            {
                ReceptListView.ItemsSource = DbHelper.SelectedCategory.Recipes;
            }

        }

        /// <summary>
        /// SelectionChangesEvent for ReceptListView. Sets selectedRecipe and opens the recipe in RecipeWindow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReceptListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DbHelper.SelectedRecipe = ReceptListView.SelectedItem as Recipe;
            ReceptWindow receptWindow = new ReceptWindow();
            receptWindow.Show();
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

        /// <summary>
        /// Click event for AddWindowBtn. Opens AddWindow and closes this window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            AddReceptWindow addReceptWindow = new AddReceptWindow();
            addReceptWindow.Show();
            this.Close();
        }

        /// <summary>
        /// Click event for CategoryWindowBtn. Opens KategoriWindow and closes this window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategoryWindow_Click(object sender, RoutedEventArgs e)
        {
            KategoriWindow kategoriWindow = new KategoriWindow();
            kategoriWindow.Show();
            this.Close();

        }

        /// <summary>
        /// Click event for RandomRecipeBtn. Starts RandomRecipe method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RandomRecipeBtn_Click(object sender, RoutedEventArgs e)
        {
            RandomRecipe();
        }

        /// <summary>
        /// Click event for SearchRecipeBtn. Starts SearchRecipe method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchRecipeBtn_Click(object sender, RoutedEventArgs e)
        {
            SearchRecipe();
        }
    }
}
