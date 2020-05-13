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
        private readonly string _customImage = "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAQAAAAAYLlVAAAABGdBTUEAALGPC/xhBQAAACBjSFJNAAB6JgAAgIQAAPoAAACA6AAAdTAAAOpgAAA6mAAAF3CculE8AAAAAmJLR0QAAKqNIzIAAAAJcEhZcwAADsQAAA7EAZUrDhsAAAAHdElNRQfkBQwSDQRuVg62AAAEm0lEQVRo3sWZTWxUVRTHf2Pph/bDTnQ67UACmmqVLlBREROLRRciogsNIcVWNJi40W401lYjumBta0jEaIINblwR8APdQFLpR0DUkCIUommUTmeaWrG2FqedcTFnbu97M+/N3DfM9Lxk5r3z9T/349x37n2wwuTLy7qFZ4AjDKxM8F0k5HpzJeCbiKkAYjQVP4ADCj5BggPFhq/mqiWAWW725ugGjwG8RA0AV7gCQBV7itl+Hxel5T28LXeXPTfGA20X0AWCBPhXnp4sXgDHBfIQAJ/J0zfFgr+DJYF8AID75CnOXcUJ4EMBPKU4Q8LpKwb8cgLuUrw24fztNRlN6DUBm6BM8Ur5Q7ivFhrexwWBesfCf1e4Y4VOxm0CdI16C7+OBZE8UdgAvhaY/jTJYZF8VUj4RpWAD6bJNqlkLOCbsVdAhjNKR0T6QaHgq/hLIHZnlLerZKzJ3Wn2kqyEOgKEqKNV3niTrOW/DJrljBME4BAniDLBFFGWTALwE8JPg/ptIESAVTab99jn4O19W3ICLDBBmAnCzKjf34npKhv5nHNEiFtKDKdr1paAOjXwT04+4kQ4x2E2AmzhWk5GqfzfhRvtNvK2QIuPQTZnmQVRpogQZopfOcp4Fu3beZp1MmvquDWL9qCPOW7KABUmQpQwUaIs4p1KqSNIAwHqqSdAA0FLWPNwXrpjsYhV3QssCuoodGhTo7so8G9p070d4HWNcTAt6a4vreIjrcFvpNjPMq/Y35msY4ZUyTEtA9p00WamlOhn1hQEvp7TCuNPttjFjYwp8W/cfd3hG7mUzX+WCPMiaw+vdlKr5Ki25rWZILiSPse+dZ9jJdquN+740jGjTlXGJPiUUjODT/JMyxK1izBq0HNqv5fgONWe4Sv4QhvS501MW5lRpiP4PcH7GVY+Zmg1NW9mXJkf8RTA8oQep9mLgxBn1egFjK2DCv4sIU8NAKpVhdNobHunWM7lMYdYK07mc0keG5UyJ9br3NTcd3IPy/+ItYjMiWKctnnJI4BBY3jdyrXkyy2AIU8BDNm8GFOlnIXGs5aWmekWKXQWqfIWwFaZROe9tkCdJWx1VnEbgvxmgG7pMghuATwk/8OOGn52stNloc4hAGfyMS0duD6jvIwuZmWz1qWdF+nUrMobDwc3612NWxm1bLLG2ObaCA/vgr1i+mWaZDX9GXd6xzKseakjnZedYJy7JrV8WNeAcnoYS24nAJhmWt0/xSjdlFv0c1qMMlMqhR7VeI+pjVyCBEv0E8BPr9poJUhwyXJonUrlC6bwqUUkphaRNbaOP6OyBO7llG0wbhPJ8mJm+ELfIa6SL5QyOmXGJ69pOimx6PvoIKJpzLOPCgB+EM4OswD2i1kv8Di/2Do+89JcaxuMy2xn+Wh7v1kAJ8WsWysrkz2yydXuHr63DUaP3J00gfepYkI/N4rwYg6naj72MKlZpTzMmXwkvTHtwGqRPmpztq+lT/uqmAqkwqQPfrIYD7DBxBiADQxYfPxoZt6idnNhOjx+YfbRTlgNwCNOSk7UxCuEOMPHXPUEn6Qa9nI/kxzkYh5eCkn/A6hm5iTbRLelAAAAJXRFWHRkYXRlOmNyZWF0ZQAyMDIwLTA1LTEyVDE4OjEzOjA0KzAwOjAwK+p3owAAACV0RVh0ZGF0ZTptb2RpZnkAMjAyMC0wNS0xMlQxODoxMzowNCswMDowMFq3zx8AAAAZdEVYdFNvZnR3YXJlAHd3dy5pbmtzY2FwZS5vcmeb7jwaAAAAAElFTkSuQmCC";
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
                    conn.Insert(new Category() { Name = input, Icon = _customImage });

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
