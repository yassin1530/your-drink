using System;
using System.Collections.Generic;
using YourDrink.Model;
using Xamarin.Forms;
using SQLite;
using System.Linq;

using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using YourDrink.Redefinitions;

namespace YourDrink
{
    public partial class CategoryListPage : ContentPage
    {
        public static CategoryListPage That;
        private Array Categorys { get; set; }
        public static Category ActiveCategory { get; set; }
        public CategoryListPage()
        {

            InitializeComponent();

            That = this;

            FillCategoryList();
            
        }

        public async void AddCategory(System.Object sender, System.EventArgs e)
        {
            string input = await DisplayPromptAsync("Neue Kategorie", "", maxLength: 20);

            if (input != null)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
                {
                    conn.Insert(new Category() { Name = input });

                    FillCategoryList();
                }
            }
        }
        private void FillCategoryList()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {


                Categorys = conn.Query<CategoryCount>(@"SELECT c.*, COUNT(d.CategoryId) AS Count
                                                   FROM Category AS c LEFT JOIN Drink AS d
                                                   ON c.Id = d.CategoryId GROUP BY d.CategoryId").ToArray();

                CategoryList.ItemsSource = Categorys;
            }
        }

       public void ItemTapped(System.Object sender, EventArgs e)
        {
            //int categoryId = (e.Item as CategoryCount).Id;
            // int categoryId = (((CustomViewCell)sender).BindingContext as CategoryCount).Id;
          int categoryId = ((CategoryCount)CustomViewCell.Binding).Id;
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                // ActiveCategory = new Category();
                ActiveCategory = conn.Get<Category>(categoryId);

                MainPage.NavToDrinkPage();

            }
        }

        public void AskForDelete(object sender, System.Timers.ElapsedEventArgs e)//
        {
            Device.BeginInvokeOnMainThread(async() =>
            {
                string categoryName = ((CategoryCount)CustomViewCell.Binding).Name;
                var answer = await DisplayActionSheet($"Kategorie {categoryName} löschen?", "Abbrechen", "Ok");

                if(answer == "Ok")
                {
                    int id = ((CategoryCount)CustomViewCell.Binding).Id;

                    using(var conn = new SQLiteConnection(App.DatabasePath))
                    {
                        conn.Delete<Category>(id);
                    }
                    FillCategoryList();
                }
            });
           

        }

        async void MultiGestureView_LongPressed()
        {

            //var answer = await DisplayActionSheet("Löschen?", "Abbrechen", "Ok");
        }

        void ViewCell_Tapped(System.Object sender, System.EventArgs e)
        {

           /* var xxx = sender as Android.Views.View;
           
            Device.BeginInvokeOnMainThread(async () =>
            {

                var answer = await DisplayActionSheet("Löschen?", "Abbrechen", "Ok");
            });*/
        }
        public static void Abc()
        {

        }
    }

}
