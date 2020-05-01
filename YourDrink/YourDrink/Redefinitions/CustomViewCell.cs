using System;
using System.Timers;
using Xamarin.Forms;

namespace YourDrink.Redefinitions
{
    public class CustomViewCell : ViewCell
    {

        public static object Binding { get; set; }
      

        public Timer Timer;

        public event ElapsedEventHandler ItemOnHold;

        public event EventHandler ItemClicked;

        private bool TimerElapsed = false;

        public void OnPressed()
        {
            Binding = BindingContext;
            Timer = new Timer(700) { AutoReset = false, Enabled = true };
            Timer.Elapsed += ItemOnHold;
            Timer.Elapsed += (object sender, ElapsedEventArgs e) =>
            {
                TimerElapsed = true;
            };
            Timer.Start();
        }

        public void OnReleased()
        {
            Timer.Elapsed -= ItemOnHold;
            Timer.Stop();
            if (!TimerElapsed)
            {
               
                ItemClicked(this, new EventArgs());
            }
            TimerElapsed = false;
 
        }
      
    }
}
