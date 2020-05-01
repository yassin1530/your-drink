using System;
using YourDrink.Redefinitions;
using Android;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using static Android.Views.View;
using Android.Content;
using Android.Views;
using YourDrink.Droid;
using Android.Widget;

[assembly: ExportRenderer(typeof(Xamarin.Forms.ListView), typeof(CustomListViewRenderer))]
namespace YourDrink.Droid
{

    public class CustomListViewRenderer : ListViewRenderer
    {
        Context _context;

        public CustomListViewRenderer(Context context) : base(context)
        {

            _context = context;
           
        }

  
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {


            base.OnElementChanged(e);

            var customListView = e.NewElement as CustomListView;

            //customListView.Context = _context;

            var thisListView = Control as Android.Widget.ListView;


            //thisListView.SetBindingContext(_context);
          
       /* thisListView.Touch += (object sender, TouchEventArgs args) =>
            {
               
                if (args.Event.Action == MotionEventActions.Down)
                {
                  
                    customListView.OnPressed();
                    
                }
                else if (args.Event.Action == MotionEventActions.Up)
                {
                    //Control.Adapter = new NativeAndroidListViewAdapter(_context as Android.App.Activity, customListView);
                    customListView.OnReleased();
                
                }
            };*/
            thisListView.ItemClick += ThisListView_ItemClick;
        }

        private void ThisListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            CategoryListPage.Abc();
        }
    }
}
