using System;
using Android.Content;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using YourDrink.Droid;
using YourDrink.Redefinitions;
using static Android.Views.View;

[assembly: ExportRenderer(typeof(CustomViewCell), typeof(CustomViewCellRenderer))]
namespace YourDrink.Droid
{
    public class CustomViewCellRenderer : ViewCellRenderer
    {
        

        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
        {
             var cell = base.GetCellCore(item, convertView, parent, context);


             var customViewCell = item as CustomViewCell;

             cell.Touch += (object sender, TouchEventArgs args) =>
             {

                 if (args.Event.Action == MotionEventActions.Down)
                 {

                     customViewCell.OnPressed();

                 }
                 else if (args.Event.Action == MotionEventActions.Up)
                 {
                     //Control.Adapter = new NativeAndroidListViewAdapter(_context as Android.App.Activity, customListView);
                     customViewCell.OnReleased();

                 }
             };

            return cell;
        }

    }
}
