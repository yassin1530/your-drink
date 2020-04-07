using System;
using Xamarin.Forms;
using YourDrink.Model;
using SQLite;
using System.Collections.Generic;

namespace YourDrink
{
    public class Favorite
    {
        public static readonly string BorderIcon = "outline_grade_white_24";
        public static readonly string FilledIcon = "baseline_grade_white_24";

        public static bool IsFavorite { get; set; } = false;
    
        public DrinkDetail Detail { get; set; }
        public Drink Drink { get; set; }
        public static ToolbarItem Item { get; set; } 

        public Favorite()
        {
            Item = new ToolbarItem();

            try
            {
                Drink = DrinkPage.ActiveDrink;

                using (var conn = new SQLiteConnection(App.DatabasePath))
                {

                    Detail = conn.Table<DrinkDetail>().Where(detail => detail.DrinkId == Drink.Id).First();
                }
            }
            catch { return; }
     
        }
        public void SetIconOnNav()
        {
            if (Detail.Favorite == 1)
            {
                Item.IconImageSource = FilledIcon;
                DeleteItemEvents();
                Item.Clicked += SetToNonFavorite;
            }
            else
            {
                Item.IconImageSource = BorderIcon;
                Item.Clicked += SetToFavorite;
            }

            var items = MasterDetail.That.ToolbarItems;

            if (!items.Contains(Item))
            {
                items.Add(Item);
            }
        }
        public void SetToFavorite(object sender, EventArgs e)
        {
            Item.IconImageSource = FilledIcon;
            DeleteItemEvents();
            
            Item.Clicked += SetToNonFavorite;
            ChangeFavoriteState(1);
            DependencyService.Get<IMessage>().ShortAlert("Zu Favoriten hinzugefügt");
        }

        public void SetToNonFavorite(object sender, EventArgs e)
        { 
            Item.IconImageSource = BorderIcon;
            DeleteItemEvents();
            Item.Clicked += SetToFavorite;
            ChangeFavoriteState(0);
            DependencyService.Get<IMessage>().ShortAlert("Aus Favoriten entfernt");
        }

        // 1 = Favorite , 0 = Kein Favorite
        private void ChangeFavoriteState(int state)
        {
            using (var conn = new SQLiteConnection(App.DatabasePath))
            {
                Detail.Favorite = state;
                conn.Update(Detail);
            }
        }
        public void RemoveFavoriteIcon()
        {
            var items = MasterDetail.That.ToolbarItems;

            items.Remove(Item);
        }
        public void DeleteItemEvents()
        {
            Item.Clicked -= SetToFavorite;
            Item.Clicked -= SetToNonFavorite;
        }

        public List<DrinkWithImage> GetFavoriteDrinks()
        {
            using (var conn = new SQLiteConnection(App.DatabasePath))
            {
                var drinks = conn.Query<DrinkWithImage>($@"SELECT * FROM DrinkDetail AS dd
                                                        LEFT JOIN Drink ON dd.DrinkId = Drink.Id WHERE dd.Favorite = 1 ");
                //var favorites = conn.Table<DrinkDetail>().Where(detail => detail.Favorite == 1);

                //var drinks = new List<Drink>();

               /* foreach(var favorite in favorites)
                {
                    var drink = conn.Get<Drink>(d => d.Id == favorite.DrinkId);

                    drinks.Add(drink);
                }*/

                return drinks;
            }
        }
        
    }
}
