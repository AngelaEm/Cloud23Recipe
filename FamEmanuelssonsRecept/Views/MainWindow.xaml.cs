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

        private void KategoriListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DbHelper.SelectedCategory = KategoriListView.SelectedItem as Category;

            if (DbHelper.SelectedCategory != null)
            {
                ReceptListView.ItemsSource = DbHelper.SelectedCategory.Recipes;
            }

        }

        private void ReceptListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DbHelper.SelectedRecipe = ReceptListView.SelectedItem as Recipe;
            ReceptWindow receptWindow = new ReceptWindow();
            receptWindow.Show();
            this.Close();

        }

        private void RandomRecipe()
        {
            Random random = new Random();
            List<Recipe> recipes = DbHelper.LoadRecipes();
            var randomInex = random.Next(1, recipes.Count+1);

            Recipe randomRecipe = recipes.FirstOrDefault(r => r.Id == recipes[randomInex].Id);

            DbHelper.SelectedRecipe = randomRecipe;

            ReceptWindow receptWindow = new ReceptWindow();
            receptWindow.Show();
            this.Close();

        }

        private void SearchRecipe()
        {
            var searchWord = SearchRecipeTextBox.Text;
            List<Recipe> recipes = DbHelper.LoadRecipes();
            List<Recipe> selectedRecipes = new List<Recipe>();
            var searchResult = recipes.FindAll(r => r.Ingredients.ToLower().Contains(searchWord.ToLower()));
            //foreach (var r in recipes)
            //{
            //    if (r.Ingredients.ToLower().Contais(searchWord.ToLower()))
            //    {
            //        selectedRecipes.Add(r);
            //    }
            //}
            //ReceptListView.ItemsSource = selectedRecipes;

                ReceptListView.ItemsSource = searchResult.ToList();


        }

        private void EditWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            EditWindow editWindow = new EditWindow();
            editWindow.Show();
            this.Close();

        }

        private void AddWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            AddReceptWindow addReceptWindow = new AddReceptWindow();
            addReceptWindow.Show();
            this.Close();
        }

        private void CategoryWindow_Click(object sender, RoutedEventArgs e)
        {
            KategoriWindow kategoriWindow = new KategoriWindow();
            kategoriWindow.Show();
            this.Close();

        }

        private void RandomRecipeBtn_Click(object sender, RoutedEventArgs e)
        {
            RandomRecipe();
        }

        private void SearchRecipeBtn_Click(object sender, RoutedEventArgs e)
        {
            SearchRecipe();
        }
    }
}
