using System;
using Android.App;
using Android.Widget;
using YourDrink;
using Android.Graphics;

[assembly: Xamarin.Forms.Dependency(typeof(MessageToast))]
namespace YourDrink
{
    public class MessageToast : IMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            var toast = Toast.MakeText(Application.Context, message, ToastLength.Short);
          
            toast.View.Background.SetColorFilter(Color.White, PorterDuff.Mode.SrcIn);
        
            toast.Show();
       
        }
    }

    
}