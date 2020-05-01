using System;
using System.Collections.Generic;
using YourDrink.Model;
using SQLite;
using System.Linq;

using Xamarin.Forms;
using YourDrink.Redefinitions;

namespace YourDrink
{

    public partial class DrinkPage : ContentPage
    {
        public static Drink ActiveDrink { get; set; }
        public Category ActiveCategory { get; set; }
        public static DrinkPage That { get; set; }
        public bool IsFavorite { get; set; }
        public static DrinkPage FavoritePage { get; set; }

        public DrinkPage()
        {
            InitializeComponent();
            LoadFavorites();
            IsFavorite = true;
            FavoritePage = this;
            ToolbarItems.Clear();
        }
        public void LoadFavorites()
        {
            DrinkList.ItemsSource = Favorite.GetFavoriteDrinks();
        }

        public DrinkPage(Category activeCategory)
        {
            InitializeComponent();

            ActiveCategory = activeCategory;

           // MasterDetail.MainItem.Clicked -= MasterDetail.AddCategory;
           // MasterDetail.MainItem.Clicked += AddDrink;
            That = this;
            IsFavorite = false;
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
                DrinkList.ItemsSource = conn.Query<DrinkWithImage>($"SELECT Drink.*, dd.Image FROM Drink LEFT JOIN DrinkDetail AS dd ON dd.DrinkId = Drink.Id WHERE Drink.CategoryId = {CategoryListPage.ActiveCategory.Id}");
                //= conn.Table<Drink>().Where(drink => drink.CategoryId.Equals(CategoryListPage.ActiveCategory.Id)).ToArray();
            }

        }

        public void AddDrink(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new CreateDrinkPage(), true);
        }

        void DrinkList_ItemTapped(System.Object sender, EventArgs e)
        {
            int drinkId = ((DrinkWithImage)CustomViewCell.Binding).Id;

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
        public void AskForDelete(object sender, System.Timers.ElapsedEventArgs e)//
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                string drinkName = ((DrinkWithImage)CustomViewCell.Binding).Name;
                var answer = await DisplayActionSheet($"{drinkName} löschen?", "Abbrechen", "Ok");

                if (answer == "Ok")
                {
                    int drinkId = ((DrinkWithImage)CustomViewCell.Binding).Id;

                    using (var conn = new SQLiteConnection(App.DatabasePath))
                    {
                        conn.Delete<Drink>(drinkId);
                        conn.Query<DrinkDetail>($"DELETE FROM DrinkDetail WHERE DrinkId = {drinkId}");
                        
                        if (!IsFavorite)
                        {
                            FillWithAllDrinks();
                        }
                        else
                        {
                            LoadFavorites();
                        }
                    }

                }
            });


        }
    }
}
