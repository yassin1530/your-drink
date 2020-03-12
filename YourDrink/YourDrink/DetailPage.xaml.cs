using System;
using System.Collections.Generic;
using YourDrink.Model;
using SQLite;
using Xamarin.Forms;

namespace YourDrink
{
    public partial class DetailPage : ContentPage
    {
        //public static Converter<string> Amount { get { return FormatAmound(Amount); } set { } } //{ get { if (Amount == "0") { return Amount = "abc"; } else { return Amount = "{0:0F}"; }  } set { } } //{ get => Amount = Amount == "0" ? "666" : Amount; set => Amount = Amount == "0" ? "666" : Amount; }
        public static Drink Drink { get; set; }

        public DetailPage()
        {
            InitializeComponent();
        }
        public DetailPage(Drink drink)
        {
            InitializeComponent();
            Drink = drink;
            var items = MasterDetail.that.ToolbarItems;
            var item = new ToolbarItem() { IconImageSource = "Settings" };
            item.Clicked += OpenForChange;
            //items[items.IndexOf(MasterDetail.MainItem)] = item;
            MasterDetail.MainItem = item;

          

            DrinkName.Text = drink.Name;

            using (var conn = new SQLiteConnection(App.DatabasePath))
            {
                var drinkDetail = conn.Table<DrinkDetail>().Where(detail => detail.DrinkId == drink.Id).First();
                MakingLabel.Text = drinkDetail.Making;

                DrinkImage.BindingContext = drinkDetail;
                DrinkImage.SetBinding(Image.SourceProperty, new Binding("Image", BindingMode.Default, new Base64ImageConverter(), null));


                var receptList = conn.Table<Recept>().Where(recept => recept.DrinkId.Equals(drink.Id));

                ReceptList.ItemsSource = receptList.ToArray();
            }
        }
        public static string FormatAmound(string amount)
        {

            return amount == "0" ? string.Empty : amount;
        }
        public static void OpenForChange(object sender, EventArgs e)
        {
            MainPage.NavToCreateDrinkPage(Drink);
        }
    }
}
