using System;
using SQLite;
using SQLitePCL;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using Xamarin.Forms;
using Android.Content.Res;


namespace YourDrink.Droid
{
   
    [Activity(Label = "YourDrink", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        /* public static ContentPage CurrentPage { get; set; }


         public override bool OnOptionsItemSelected(IMenuItem item)
         {
             var app = App.Current;
             if (item.ItemId == 16908332) // This makes me feel dirty.
             {
                 var navPage = ((app.MainPage.Navigation.ModalStack[0] as MasterDetailPage).Detail as ContentPage);

                 if (app != null && navPage.Navigation.NavigationStack.Count > 0)
                 {
                     int index = navPage.Navigation.NavigationStack.Count - 1;

                     var currentPage = navPage.Navigation.NavigationStack[index];


                     var vm = currentPage.BindingContext as Android.Arch.Lifecycle.ViewModel;

                     if (vm != null)
                     {

                             return true;
                     }

                 }
             }

             return base.OnOptionsItemSelected(item);
         }*/


        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            string dbName = "YourDrink.sqlite";
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string fullPath = Path.Combine(folderPath, dbName);

            string dbPath = FileAccessHelper.GetLocalFilePath("YourDrink.sqlite");
  
            LoadApplication(new App(dbPath));
            
            //LoadApplication(new App(fullPath));
        
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
