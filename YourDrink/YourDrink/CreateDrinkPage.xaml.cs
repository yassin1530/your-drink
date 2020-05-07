using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Plugin.Media;
using SQLite;
using Xamarin.Forms;
using YourDrink.Model;

namespace YourDrink
{
    public partial class CreateDrinkPage : ContentPage
    {

        public ObservableCollection<Recept> Collection { get; set; } = new ObservableCollection<Recept>();
        private static Drink Drink { get; set; }
        private DrinkDetail DrinkDetail { get; set; }
        public static CreateDrinkPage That { get; set; }
        public static List<int> ReceptsToRemove { get; set; } = new List<int>();
        private static bool SomethingGotChanged { get; set; }
        private static bool SetupIsReady { get; set; }

        public CreateDrinkPage()
        {

            InitializeComponent();

            using(var conn = new SQLiteConnection(App.DatabasePath))
            {
                var drinkId = conn.Query<Drink>("SELECT Id FROM Drink ORDER BY Id DESC LIMIT 0,1");
                var detailId = conn.Query<DrinkDetail>("SELECT Id FROM Drink ORDER BY id DESC LIMIT 0,1");

                int newDetailId = detailId[0].Id + 1;
                int newId = drinkId[0].Id + 1;

                conn.Insert(new DrinkDetail() { Id = newDetailId, DrinkId = newId });
                conn.Insert(new Drink() { Id = newId, Name = "" });
                Drink = conn.Get<Drink>(newId);
            }

            DrinkPage.ActiveDrink = Drink;

            DrinkDetail = new DrinkDetail();
            DrinkDetail.Favorite = 1;
            SetupIsReady = false;
            That = this;
  
            SomethingGotChanged = false;

            SetTimer();
        }

        public CreateDrinkPage(Drink drink)
        {
            InitializeComponent();

            SetupIsReady = false;

            Drink = drink;

            DrinkName.Text = drink.Name;

            That = this;

            SomethingGotChanged = false;

            using (var conn = new SQLiteConnection(App.DatabasePath))
            {
                DrinkDetail = conn.Table<DrinkDetail>().Where(detail => detail.DrinkId == drink.Id).First();
                MakingLabel.Text = DrinkDetail.Making;

                DrinkImage.BindingContext = DrinkDetail;
                DrinkImage.SetBinding(Image.SourceProperty, new Binding("Image", BindingMode.Default, new Base64ImageConverter(), null));
                


                GetRecepts();

                ReceptList.ItemsSource = Collection;

                // Größe anpassen damit man nicht scrollen muss : Letzter Summand = Button
                ReceptList.HeightRequest = Collection.Count * ReceptList.RowHeight + 110;
        
            }
            SetTimer();
        }

        private void SetSetupTrue(object sender, EventArgs e)
        {
            SetupIsReady = true;
        }
        private void SetTimer()
        {
            var timer = new System.Timers.Timer(1000) { AutoReset = false };
            timer.Elapsed += SetSetupTrue;
            timer.Start();

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


        void DeleteRecept(System.Object sender, System.EventArgs e)
        {
            var id = Convert.ToInt32(((ImageButton)sender).ClassId);

            /*using (var conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.Delete<Recept>(id);
            }*/
            ReceptsToRemove.Add(id);

            Recept receptToRemove = new Recept();

             foreach(var item in Collection)
             {
                 if(item.Id == id)
                 {
                     receptToRemove = item;
                 }
             }
        
            Collection.Remove(receptToRemove);
            SomethingGotChanged = true;
           // GetRecepts();
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

 

        async void SelectImage(System.Object sender, System.EventArgs e)
        {

            Stream stream = await DependencyService.Get<IImageSelector>().GetImageStreamAsync();
            if (stream != null)
            {


                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);

                    byte[] data = ms.ToArray();

                    string base64String = Convert.ToBase64String(data);

                    /*using (var conn = new SQLiteConnection(App.DatabasePath))
                    {
                        conn.Query<DrinkDetail>($"UPDATE DrinkDetail SET Image = '{base64String}'");
                    }*/

                    SomethingGotChanged = true;

                    DrinkDetail.Image = base64String;

                    DrinkImage.SetBinding(Image.SourceProperty, new Binding("Image", BindingMode.Default, new Base64ImageConverter(), null));

                }

            }


        }

        void Picker_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
        }

        public static void CloseForChange(object sender, EventArgs e)
        {
            MainPage.NavToDetailPage();
        }

        public async void AskForSave()
        {
            if (SomethingGotChanged)
            {

                bool answer = await DisplayAlert("", "Änderungen speichern?", "Ja", "Nein");

                if (answer)
                {
                    SaveChanges();
                }
            }

                MainPage.NavToDetailPage();
            
        }

        public static void SaveChanges ()
        {
            using (var conn = new SQLiteConnection(App.DatabasePath))
            {
                conn.UpdateAll(That.Collection);


                Drink.Name = That.DrinkName.Text;
                conn.Update(Drink);

               
                That.DrinkDetail.Making = That.MakingLabel.Text;
                conn.Update(That.DrinkDetail);

                foreach(int id in ReceptsToRemove)
                {
                    conn.Delete<Recept>(id);
                }
                ReceptsToRemove.Clear();
            }
        }
        public static void AcceptPressed (object sender, EventArgs e)
        {
            SaveChanges();
            MainPage.NavToDetailPage();
        }
        public void SomethingChanged (object sender, EventArgs e)
        {
            if (SetupIsReady)
            {
                SomethingGotChanged = true;
            }
        }
    }
}
