using System;
using System.Collections.Generic;
using YourDrink.Model;
using Xamarin.Forms;
using SQLite;
using System.Linq;

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


        void CategoryButton_Clicked(System.Object sender, EventArgs e)
        {
            var button = (sender as Button);

            int categoryId = Convert.ToInt32(button.ClassId.Substring(button.ClassId.Length - 1));


            using (SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {
                ActiveCategory = new Category();
                ActiveCategory = conn.Get<Category>(categoryId);

                MainPage.NavToDrinkPage(ActiveCategory);

            }


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
    }

}
