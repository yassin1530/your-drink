using System;
using System.Collections.ObjectModel;
using SQLite;
using Xamarin.Forms;
using YourDrink.Model;


namespace YourDrink
{
    public partial class CreateDrinkPage : ContentPage
    {

        public ObservableCollection<Recept> Collection { get; set; } = new ObservableCollection<Recept>();
        private Drink Drink { get; set; }

        public CreateDrinkPage()
        {

            InitializeComponent();
        }
        public CreateDrinkPage(Drink drink)
        {
            InitializeComponent();

            Drink = drink;

            DrinkName.Text = drink.Name;



            using (var conn = new SQLiteConnection(App.DatabasePath))
            {
                var drinkDetail = conn.Table<DrinkDetail>().Where(detail => detail.DrinkId == drink.Id).First();
                MakingLabel.Text = drinkDetail.Making;

                DrinkImage.BindingContext = drinkDetail;
                DrinkImage.SetBinding(Image.SourceProperty, new Binding("Image", BindingMode.Default, new Base64ImageConverter(), null));



                GetRecepts();

                ReceptList.ItemsSource = Collection;

                // Größe anpassen damit man nicht scrollen muss : Letzter Summand = Button
                ReceptList.HeightRequest = Collection.Count * ReceptList.RowHeight + 110;

            }
        }

        void AddRecept(System.Object sender, System.EventArgs e)
        {
            using (var conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.Insert(new Recept() { Name = String.Empty, DrinkId = Drink.Id });
                GetRecepts();
            }

            ReceptList.HeightRequest = Collection.Count * ReceptList.RowHeight + 110;
        }

       /* void ContentPage_Disappearing(System.Object sender, System.EventArgs e)
        {
            using (var conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.UpdateAll(Collection);
            }

        }*/

        void DeleteRecept(System.Object sender, System.EventArgs e)
        {
            var id = Convert.ToInt32(((ImageButton)sender).ClassId);

            using (var conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.Delete<Recept>(id);
            }

            GetRecepts();
        }
        private void GetRecepts()
        {
            Collection.Clear();

            using (var conn = new SQLiteConnection(App.DatabasePath))
            {

                var receptList = conn.Table<Recept>().Where(recept => recept.DrinkId.Equals(Drink.Id)).ToList();


                foreach (var recept in receptList)
                {
                    Collection.Add(recept);
                }
            }
        }

        void UpdateRecepts(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            var entry = sender as Entry;
            int id = Convert.ToInt16(entry.ClassId.Substring(entry.ClassId.Length - 1));
            //string type = entry.ClassId.Substring(0, entry.ClassId.Length - 1);

            var updateRecept = new Recept();    
        
            foreach(var recept in Collection)
            {
                updateRecept = recept.Id == id ? recept : null;
            }

            using (var conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.Update(updateRecept); 
            }
        }
        void UpdateMaking(object sender, EventArgs e)
        {
            using(var conn = new SQLiteConnection(App.DatabasePath))
            {
                var drinkDetail = conn.Table<DrinkDetail>().Where(detail => detail.DrinkId == Drink.Id).First();

                drinkDetail.Making = MakingLabel.Text;

                conn.Update(drinkDetail);
            }
        }

        void UpdateDrinkName(object sender, FocusEventArgs e)
        {
            using(var conn = new SQLiteConnection(App.DatabasePath))
            {
                Drink.Name = DrinkName.Text;

                conn.Update(Drink);
            }
        }
    }
}
