using System;
using System.Drawing;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific.AppCompat;
using YourDrink.Model;

namespace YourDrink
{
    public class BuildNavigationHeader
    {
        public Xamarin.Forms.NavigationPage Page { get; }

        private ToolbarItem item
        {
            get
            {
                var item = new ToolbarItem()
                {
                   
                };
        
                return item;
            }
        }
        public BuildNavigationHeader()
        {
        }
        public static Xamarin.Forms.NavigationPage BuildPage(Type type, object obj = null)
        {
            object pageType;

            if(obj == null)
            {
                pageType = Activator.CreateInstance(type);
            }
            else
            {
                pageType = Activator.CreateInstance(type, obj);
            }
 

            var page = new Xamarin.Forms.NavigationPage((Xamarin.Forms.Page)pageType)
            {
                BarBackgroundColor = Xamarin.Forms.Color.FromHex("#4d0018")
            };
            page.On<Android>().SetBarHeight(120);

            var item = new ToolbarItem()
            {
               
            };
            item.Clicked += CategoryListPage.that.AddCategory_Clicked;

            page.ToolbarItems.Add(item);

            return page;
        }


        public BuildNavigationHeader(int type)
        {
            var page = new Xamarin.Forms.NavigationPage();

            if (type == 0)
            {
                page = new Xamarin.Forms.NavigationPage(new MainPage());
            }
            else if (type == 1)
            {
                //page = new Xamarin.Forms.NavigationPage(new MainPage(new Model.Category()));
            }

            page.BarBackgroundColor = Xamarin.Forms.Color.FromHex("#4d0018");
            page.On<Android>().SetBarHeight(120);

            page.ToolbarItems.Add(this.item);


            this.Page = page;
        }
        public BuildNavigationHeader(Category category)
        {
            var page = new Xamarin.Forms.NavigationPage();



            //page = new Xamarin.Forms.NavigationPage(new MainPage(category));


            page.BarBackgroundColor = Xamarin.Forms.Color.FromHex("#4d0018");
            page.On<Android>().SetBarHeight(120);

            page.ToolbarItems.Add(this.item);


            this.Page = page;
        }


    }
}
