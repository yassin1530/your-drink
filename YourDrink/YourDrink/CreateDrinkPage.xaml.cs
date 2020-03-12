using System;
using System.Collections.Generic;
using SQLite;
using Xamarin.Forms;
using YourDrink.Model;

namespace YourDrink
{
    public partial class CreateDrinkPage : ContentPage
    {

        public TableQuery<Recept> Recept { get; set; }

        public CreateDrinkPage(Drink drink)
        {
            InitializeComponent();

        

            DrinkName.Text = drink.Name;

            using (var conn = new SQLiteConnection(App.DatabasePath))
            {
                var drinkDetail = conn.Table<DrinkDetail>().Where(detail => detail.DrinkId == drink.Id).First();
                MakingLabel.Text = drinkDetail.Making;

                DrinkImage.BindingContext = drinkDetail;
                DrinkImage.SetBinding(Image.SourceProperty, new Binding("Image", BindingMode.Default, new Base64ImageConverter(), null));


                Recept = conn.Table<Recept>().Where(recept => recept.DrinkId.Equals(drink.Id));

                ReceptList.ItemsSource = Recept.ToArray();
            }
        }

        void AddIngredient_Clicked(System.Object sender, System.EventArgs e)
        {
          
        }
    }
}
