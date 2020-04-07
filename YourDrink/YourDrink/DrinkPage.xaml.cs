using System;
using System.Collections.Generic;
using YourDrink.Model;
using SQLite;
using System.Linq;

using Xamarin.Forms;

namespace YourDrink
{

    public partial class DrinkPage : ContentPage
    {
        public static Drink ActiveDrink { get; set; }
        public Category ActiveCategory { get; set; }
        public static DrinkPage That { get; set; }

        public DrinkPage()
        {
            InitializeComponent();
            LoadFavorites();

        }
        public void LoadFavorites()
        {
            DrinkList.ItemsSource = new Favorite().GetFavoriteDrinks();
        }

        public DrinkPage(Category activeCategory)
        {
            InitializeComponent();

            ActiveCategory = activeCategory;

            MasterDetail.MainItem.Clicked -= MasterDetail.AddCategory;
            MasterDetail.MainItem.Clicked += AddDrink;
            That = this;
        }

       /* public DrinkPage(List<Drink> drinks)
        {
            InitializeComponent();

            DrinkList.ItemsSource = drinks;
        }*/

        public void FillWithAllDrinks()
        {
           

            using (var conn = new SQLiteConnection(App.DatabasePath))
            {//WHERE Drink.CategoryId = {CategoryListPage.ActiveCategory.Id}
                DrinkList.ItemsSource = conn.Query<DrinkWithImage>($"SELECT * FROM Drink LEFT JOIN DrinkDetail AS dd ON dd.DrinkId = Drink.Id WHERE Drink.CategoryId = {CategoryListPage.ActiveCategory.Id}");
                 //= conn.Table<Drink>().Where(drink => drink.CategoryId.Equals(CategoryListPage.ActiveCategory.Id)).ToArray();
            }
            Favorite.IsFavorite = false;
        }

        public void AddDrink(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new CreateDrinkPage(), true);
        }

        void DrinkList_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            int drinkId = (e.Item as DrinkWithImage).Id;

            using (var conn = new SQLiteConnection(App.DatabasePath))
            {
                var drink = conn.Get<Drink>(drinkId);
                // Für Favorite weil CategoryListPage noch nicht aufgerufen wurde
                if (CategoryListPage.ActiveCategory == null)
                {
                    CategoryListPage.ActiveCategory = conn.Get<Category>(category => category.Id == drink.CategoryId);
                }

                ActiveDrink = drink;
                MainPage.NavToDetailPage();
            }
        }
    }
}
