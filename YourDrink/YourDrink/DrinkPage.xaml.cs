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
        public Category ActiveCategory { get; set; } 
        public static DrinkPage That { get; set; }

        public DrinkPage(Category activeCategory)
        {
            InitializeComponent();

            ActiveCategory = activeCategory;

            MasterDetail.MainItem.Clicked -= MasterDetail.AddCategory;
            MasterDetail.MainItem.Clicked += AddDrink;
            That = this;
        }
        public void FillWithAllDrinks()
        {
            using (var conn = new SQLiteConnection(App.DatabasePath))
            {         
                DrinkList.ItemsSource = conn.Table<Drink>().Where(drink => drink.CategoryId.Equals(ActiveCategory.Id)).ToArray();
            }
            Favorite.IsFavorite = false;
        }
       public void FillWithFavoriteDrinks()
        {
            using (var conn = new SQLiteConnection(App.DatabasePath))
            {
              
                var drinks = conn.Table<Drink>().Where(drink => drink.CategoryId.Equals(ActiveCategory.Id));
                var ids = new List<int>();

                foreach(Drink drink in drinks)
                {
                    ids.Add(drink.Id);
                }
                // Zuerst alle Drinks anhand der Id von Drinks finden und alle nehmen die Favoriten sind
                var favoriteDrinks = conn.Table<DrinkDetail>().Where(detail => ids.Contains(detail.DrinkId) || detail.Favorite == 1 );
                // Wenn sie gefunden wurden anhand den Ids der Favoritendrinks die Drinks filtern
                ids.Clear();
                foreach(DrinkDetail detail in favoriteDrinks)
                {
                    ids.Add(detail.DrinkId);
                }

                DrinkList.ItemsSource = drinks.Where(drink => ids.Contains(drink.Id)).ToArray();
            }
            Favorite.IsFavorite = true;
        }

        void DrinkButton_Clicked(System.Object sender, System.EventArgs e)
        {
            var button = (sender as Button);

            int drinkId = Convert.ToInt32(button.ClassId.Substring(button.ClassId.Length - 1));

            using (var conn = new SQLiteConnection(App.DatabasePath))
            {
                var drink = conn.Get<Drink>(drinkId);

                MainPage.NavToDetailPage(drink, Favorite.IsFavorite);
            }

        }
        public void AddDrink(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new CreateDrinkPage(), true);
        }
    }
}
