using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Timers;
using Xamarin.Forms;
using YourDrink.Model;

namespace YourDrink
{
    public partial class PostPage : ContentPage
    {
        public PostPage()
        {
            InitializeComponent();
            LoadPosts();
        }
        public async void LoadPosts()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            var httpClient = new HttpClient(handler);

            try
            {
                /*var timer = new Timer(5000) { AutoReset = false, Enabled = true };
                timer.Elapsed += (object sender, ElapsedEventArgs e) =>
                {
                    ConnectionFailed();
                };*/

                var response = await httpClient.GetStringAsync("https://192.168.2.127:5001/Post");
                var posts = JsonConvert.DeserializeObject<List<Post>>(response);
                PostList.ItemsSource = posts;

            }
            catch
            {
                ConnectionFailed();
            }




        }
        private void ConnectionFailed()
        {

            Content = new Label()
            {
                Text = "Laden Fehlgeschlagen",
                TextColor = Color.Gray,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
        }
    }
}
