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
        public static MasterDetail that { get; set; }
        public static ToolbarItem MainItem { get { return MainItem; } set => that.MainToolbarItem = MainItem; }

        public MasterDetail()
        {
            InitializeComponent();

            var page = new Xamarin.Forms.NavigationPage(new MainPage())
            {
                BarBackgroundColor = Xamarin.Forms.Color.FromHex("#4d0018")
            };
            page.On<Android>().SetBarHeight(120);



            masterDetailPage.Detail = page;
            MainItem = MainToolbarItem;
            that = this;
            
        }

        public void OpenSideBar()
        {
            masterDetailPage.IsPresented = true;
        }

       public void AddCategory(System.Object sender, System.EventArgs e)
        {
            CategoryListPage.That.AddCategory(sender, e);
        }
    }
}