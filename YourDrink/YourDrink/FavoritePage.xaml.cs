using System;
using System.Collections.Generic;
using SQLite;
using Xamarin.Forms;
using YourDrink.Model;

namespace YourDrink
{
    public partial class FavoritePage : ContentPage
    {
        public FavoritePage()
        {
            //InitializeComponent();

            // FillCategoryList();
           
        
        }
        public void Activate()
        {
            //MainPage.NavToDrinkPage(true, new Favorite().GetFavoriteDrinks());
        }
        public void FillCategoryList()
        {
            using (var conn = new SQLiteConnection(App.DatabasePath))
            {
                var drinkIds = conn.Query<DrinkDetail>("SELECT DISTINCT DrinkId FROM DrinkDetail WHERE Favorite = 1");

                var categoryIds = new List<Drink>();

                foreach (var detail in drinkIds)
                {
                    categoryIds.Add(conn.Get<Drink>(detail.DrinkId));
                }

                var categories = new List<Category>();
                categories.Add(new Category() { Id = 11, Name = "test" });
                foreach (var drink in categoryIds)
                {
                    categories.Add(conn.Get<Category>(drink.CategoryId));
                }

                CategoryList.ItemsSource = categories.ToArray();

            }
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var button = (sender as Button);

            int categoryId = Convert.ToInt32(button.ClassId.Substring(button.ClassId.Length - 1));


            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
      
               var ActiveCategory = conn.Get<Category>(categoryId);

                MainPage.NavToDrinkPage(true);

            }
        }

    }
}
