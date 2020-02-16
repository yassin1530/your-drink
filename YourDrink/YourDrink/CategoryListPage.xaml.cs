using System;
using System.Collections.Generic;
using YourDrink.Model;
using Xamarin.Forms;
using SQLite;

namespace YourDrink
{
    public partial class CategoryListPage : ContentPage
    {
        public List<trash> abc;
        public CategoryListPage()
        {
          
            InitializeComponent();
            /*var a = new trash()
            {
                Id = 1,
                Name = "yassin"
            };
            var b = new trash()
            {
                Id = 2,
                Name = "nick"
            };
            var c = new trash()
            {
                Id = 3,
                Name = "pascal"
            };
            abc = new List<trash>();
            abc.Add(a);
            abc.Add(b);
            abc.Add(c);
            abc.ToArray();
            CategoryList.ItemsSource = abc;<<<<*/
            using(SQLiteConnection conn = new SQLiteConnection(App.DatabasePath))
            {


                //var categorys =  conn.Table<Category>().ToArray();
                var categorys = conn.Table<Category>();
                CategoryList.ItemsSource = categorys.ToArray();
            }
     

        }

        void CategoryButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new DrinkPage(), true);
           
        }
    }
}
