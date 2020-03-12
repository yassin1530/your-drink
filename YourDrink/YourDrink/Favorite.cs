using System;
using Xamarin.Forms;
using YourDrink.Model;
using SQLite;

namespace YourDrink
{
    public class Favorite
    {
        public static readonly string BorderIcon = "outline_grade_white_24";
        public static readonly string FilledIcon = "baseline_grade_white_24";

        public static bool IsFavorite { get; set; } = false;
        public ToolbarItem Item { get; set; }
        public DrinkDetail Detail { get; set; }
        public Drink Drink { get; set; }
   
        public Favorite(ToolbarItem item, Drink drink)
        {
            Item = item;
            Drink = drink;

            using (var conn = new SQLiteConnection(App.DatabasePath))
            {
                
                Detail = conn.Table<DrinkDetail>().Where(detail => detail.DrinkId == drink.Id).First();
            }
     
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
        public void DeleteItemEvents()
        {
            Item.Clicked -= SetToFavorite;
            Item.Clicked -= SetToNonFavorite;
        }
    }
}
