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
        public static bool HasLoaded = false;
        public static MainPage That { get; set; }
        public static Favorite Favorite { get; set; } 
        public static int ActivePage { get; set; } // 0 = CategoryPage, 1 = DrinkPage, 2 = DetailPage, 3 = CreateDrinkPage
   

        public MainPage()
        {
            InitializeComponent();
            That = this;

            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            HasLoaded = true;
        }
        public static void NavToDrinkPage(bool isFavorite = false, List<Drink> drinkList = null)
        {
            ActivePage = 1;
            int child = !isFavorite ? 0 : 1;

            //Type = That.Children[child].GetType();


            That.Children[child] = child == 0 ? new DrinkPage(CategoryListPage.ActiveCategory) : null;
            That.Children[child].IconImageSource = !isFavorite ? "Book" : "Star";
            That.CurrentPage = That.Children[child];

            if (!isFavorite)
            {
                ((DrinkPage)That.Children[child]).FillWithAllDrinks();
               // MasterDetail.SetMainToolbarItem("baseline_add_white_24", DrinkPage.That.AddDrink);
            }

           

        }

        public static void NavToDetailPage(bool isFavorite = false)
        {
            ActivePage = 2;
            int child = !isFavorite ? 0 : 1;
            // For Back Navigation
            //Type = That.Children[child].GetType();


       
            That.Children[child] = new DetailPage(DrinkPage.ActiveDrink)
            {
                IconImageSource = !isFavorite ? "Book" : "Star"
            };
            That.CurrentPage = That.Children[child];
            //Values = CategoryListPage.ActiveCategory;

            //Favorite = new Favorite();
           // Favorite.SetIconOnNav();

           // MasterDetail.SetMainToolbarItem("baseline_create_white_24dp", DetailPage.That.OpenForChange);

        }

        public static void NavToCreateDrinkPage(bool isNew = false, bool isFavorite = false)
        {
            ActivePage = 3;
            int child = !isFavorite ? 0 : 1;
            // For Back Navigation
            // Type = That.Children[child].GetType();

            //  MasterDetail.SetMainToolbarItem("baseline_done_white_24dp", CreateDrinkPage.AcceptPressed);

            That.Children[child] = isNew ? new CreateDrinkPage() : new CreateDrinkPage(DrinkPage.ActiveDrink);
            That.Children[child].IconImageSource = !isFavorite ? "Book" : "Star";
            That.CurrentPage = That.Children[child];
            //Values = CategoryListPage.ActiveCategory;
        }

        protected override bool OnBackButtonPressed()
        {
           /* if (Favorite != null)
            {
                Favorite.RemoveFavoriteIcon();
            }*/
            
            switch (ActivePage)
            {
                case 1:
                    Children[0] = new CategoryListPage();
                   // MasterDetail.SetMainToolbarItem("baseline_add_white_24", CategoryListPage.That.AddCategory);
                    break;
                case 2:
                    NavToDrinkPage();
                    break;
                case 3:
                    CreateDrinkPage.That.AskForSave();
                    break;
                  
            }
           
           // That.CurrentPage = That.Children[child];
            That.Children[0].IconImageSource = "Book";
            That.Children[1].IconImageSource = "Star";
            That.Children[2].IconImageSource = "Globe";
  

            //Values = null;
            // Events vom Toolbaritem müssen entfernt werden, weil sie mit einer neuen Objektreferenz nicht entfernt werden können
           // if (Favorite != null) { Favorite.DeleteItemEvents(); }
            

            base.OnBackButtonPressed();

            return true;
        }

        void FavoritePage_Appearing(object sender, EventArgs e)
        {
          
                (sender as DrinkPage).LoadFavorites();

           /* if (HasLoaded)
            {
                MasterDetail.That.SaveAndRemoveItems();
               
            }*/
        }

        void CategoryListPage_Appearing(object sender, EventArgs e)
        {
           /* if (HasLoaded)
            {
                MasterDetail.That.AddSavedItems();

            }*/
        }

 
    }
}
