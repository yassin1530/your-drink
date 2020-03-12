using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using YourDrink.Model;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific.AppCompat;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using SQLite;

namespace YourDrink
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        public static Type Type { get; set; }
        public static object Values { get; set; }
        public static MainPage That { get; set; }
        public static Favorite Favorite { get; set; }


        public MainPage()
        {
            InitializeComponent();
            That = this;
            //this.Children.Add(new DrinkPage(new Model.Category() { Id = 1}));

            this.On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }
        public static void NavToDrinkPage(Category category, bool isFavorite = false)
        {
            int child = !isFavorite ? 0 : 1;
            var childPage = That.Children[child];
            Type = That.Children[child].GetType();

            That.On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            That.Children[child] = new DrinkPage(category);
            That.Children[child].IconImageSource = !isFavorite ? "Book" : "Star";
            That.CurrentPage = That.Children[child];

            if (!isFavorite)
            {
                ((DrinkPage)That.Children[child]).FillWithAllDrinks();
            }
            else
            {
                ((DrinkPage)That.Children[child]).FillWithFavoriteDrinks();
            }

        }

        public static void NavToDetailPage(Drink drink, bool isFavorite = false)
        {
            int child = !isFavorite ? 0 : 1;
            // For Back Navigation
            Type = That.Children[child].GetType();


            That.On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);


            // var item = MasterDetail.that.Detail.ToolbarItems[0];
            var item = new ToolbarItem();

            var favorite = new Favorite(item, drink);
            favorite.SetIconOnNav();
            Favorite = favorite;
            

            That.Children[child] = new DetailPage(drink)
            {
                IconImageSource = !isFavorite ? "Book" : "Star"
            };
            That.CurrentPage = That.Children[child];
            Values = CategoryListPage.ActiveCategory;
        }

        protected override bool OnBackButtonPressed()
        {
            // Max 2 mal zurück. DrinkPage braucht Kategorie für Liste also Values hat Category Objekt
         
            int child = !Favorite.IsFavorite ? 0 : 1;

            if (Values == null)
            {
                That.Children[child] = !Favorite.IsFavorite ? new CategoryListPage() as Page : new FavoritePage();
            }
            else
            {
                That.Children[child] = (Page)Activator.CreateInstance(Type, Values);
                if (!Favorite.IsFavorite)
                {
                    ((DrinkPage)That.Children[0]).FillWithAllDrinks();
                }
                else
                {
                    ((DrinkPage)That.Children[1]).FillWithFavoriteDrinks();
                }

            }
            That.CurrentPage = That.Children[child];
            That.Children[0].IconImageSource = "Book";
            That.Children[1].IconImageSource = "Star";
            That.Children[2].IconImageSource = "Globe";
  

            Values = null;
            // Events vom Toolbaritem müssen entfernt werden, weil sie mit einer neuen Objektreferenz nicht entfernt werden können
            if (Favorite != null) { Favorite.DeleteItemEvents(); }
            

            base.OnBackButtonPressed();

            return true;
        }

        void FavoritePage_Appearing(System.Object sender, System.EventArgs e)
        {
            MasterDetail.that.Detail.ToolbarItems[0] = new ToolbarItem() { IconImageSource = "baseline_add_white_24" };
            ((FavoritePage)sender).FillCategoryList();
        }

        void CategoryListPage_Appearing(System.Object sender, System.EventArgs e)
        {
            //MasterDetail.that.Detail.ToolbarItems[0] = null;
        }
    }
}
