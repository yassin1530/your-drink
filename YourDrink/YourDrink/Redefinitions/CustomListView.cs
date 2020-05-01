using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;
namespace YourDrink.Redefinitions
{
    public class CustomListView : ListView
    {
        public Timer Timer;
        public object Context { get; set; }
        public event ElapsedEventHandler ItemOnHold;
  
        public event EventHandler<ItemTappedEventArgs> ItemClicked;

 

        public void OnPressed()
        {
            Timer = new Timer(1000) { AutoReset = false, Enabled = true };
            Timer.Elapsed += ItemOnHold;

            Timer.Start();
        }

        public void OnReleased()
        {
            Timer.Elapsed -= ItemOnHold;
            Timer.Stop();
           // ItemClicked(SelectedItem, new ItemTappedEventArgs(new object(), Context));
           // ItemClicked(this, new ItemTappedEventArgs(new object(), new object()));
            
        }
        public void LongClick()
        {
            //CategoryListPage.That.AskForDelete();
        }
       /* public static readonly BindableProperty ItemsProperty =
       BindableProperty.Create("Items", typeof(IEnumerable<DataSource>), typeof(CustomListView), new List<DataSource>());

        public IEnumerable<DataSource> Items
        {
            get { return (IEnumerable<DataSource>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public event EventHandler<SelectedItemChangedEventArgs> ItemSelected;

        public void NotifyItemSelected(object item)
        {
            if (ItemSelected != null)
            {
                ItemSelected(this, new SelectedItemChangedEventArgs(item));
            }
        }*/



    }
}
