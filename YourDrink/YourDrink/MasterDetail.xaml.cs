using System;
using System.Collections.Generic;
using SQLite;
using YourDrink.Model;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific.AppCompat;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace YourDrink
{
    public partial class MasterDetail : MasterDetailPage
    {
        public static MasterDetail That { get; set; }
        public static ToolbarItem MainItem { get; set; } //{ get { return MainItem; } set => That.MainToolbarItem = MainItem; }

        public MasterDetail()
        {
            InitializeComponent();

            var page = new Xamarin.Forms.NavigationPage(new MainPage())
            {
                BarBackgroundColor = Xamarin.Forms.Color.FromHex("#4d0018")
            };
            page.On<Android>().SetBarHeight(120);



            masterDetailPage.Detail = page;
           
         
            That = this;
            MainItem = That.MainToolbarItem;
        }

        public void OpenSideBar()
        {
            masterDetailPage.IsPresented = true;
        }

       public static void AddCategory(System.Object sender, System.EventArgs e)
        {
            CategoryListPage.That.AddCategory(sender, e);
        }
        public static void SetMainToolbarItem (string iconImageSource, EventHandler function)
        {
            That.MainToolbarItem.IconImageSource = iconImageSource;
            That.MainToolbarItem.Clicked += function;
        }
    }
}