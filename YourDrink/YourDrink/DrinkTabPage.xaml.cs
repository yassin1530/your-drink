using System;
using System.Collections.Generic;
using YourDrink.Model;
using Xamarin.Forms;

namespace YourDrink
{
    public partial class DrinkTabPage : TabbedPage
    {
        public static Category ActiveCategory { get; set; }

        public DrinkTabPage(Category category)
        {
            ActiveCategory = category;

            InitializeComponent();

            
        }
       
    }
}
