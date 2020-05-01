using System;
using System.Collections.Generic;
using SQLite;
using YourDrink.Model;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific.AppCompat;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using System.Linq;
using System.Threading.Tasks;

namespace YourDrink
{
    public partial class MasterDetail : MasterDetailPage
    {
        public static MasterDetail That { get; set; }
        public static ToolbarItem MainItem { get; set; } 
        public static List<EventHandler> Delegates { get; set; } = new List<EventHandler>();
        public static List<ToolbarItem> Items { get; set; } = new List<ToolbarItem>();


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
           // MainItem = That.MainToolbarItem;
        }

        public void OpenSideBar()
        {
            masterDetailPage.IsPresented = true;
        }

        /*public static void AddCategory(System.Object sender, System.EventArgs e)
        {
            CategoryListPage.That.AddCategory(sender, e);
        }*/

       /* public static void SetMainToolbarItem(string iconImageSource, EventHandler function)
        {
            That.MainToolbarItem.IconImageSource = iconImageSource;

            Delegates.Add(function);

            foreach (EventHandler e in Delegates)
            {
                That.MainToolbarItem.Clicked -= e;
            }

            That.MainToolbarItem.Clicked += function;
        }*/

       /* public async void SaveAndRemoveItems()
        {

            foreach (var item in ToolbarItems)
            {
                item.BindingContext = null; // This is to address the BindingContext issue; not related to this topic
                Items.Add(item); // items is just a List<ToolbarItem> type
            }

            ToolbarItems.Clear();

        }

        public void AddSavedItems()
        {
           
        
           
        }*/

    }
}