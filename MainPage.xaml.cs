using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();


            

        }

        string env;
        string email;
        string password;
        private void Button_Clicked(object sender, EventArgs e)
        {


            //DisplayAlert("Notice", url, "Okay");
            System.Diagnostics.Debug.WriteLine("Running Login Function");
            Login();

        }

        private async void Login(){

            System.Diagnostics.Debug.WriteLine("Login launched");
            string requestUrl = $"https://{env}.systemx.net/workers/auth";
            
            var formContent = new FormUrlEncodedContent(new[] {
                new KeyValuePair<String,String>("email",email),
                new KeyValuePair<String,String>("password",password),
            });
            formContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            HttpClient client = new HttpClient();

           var respone =  await client.PostAsync(requestUrl, formContent);
            System.Diagnostics.Debug.WriteLine(respone);
            var content = respone.Content;
            if (respone.IsSuccessStatusCode)
            {

                var jsonString = await content.ReadAsStringAsync();
                DisplayAlert("cookie?", jsonString, "Okay");
                System.Diagnostics.Debug.WriteLine(jsonString);


            }

            else
            {

                var errMessage = await content.ReadAsStringAsync();

                DisplayAlert("Error", errMessage, "Okay");
                System.Diagnostics.Debug.WriteLine(errMessage);
            }
        
        }

        private void Envernment_Completed(object sender, TextChangedEventArgs e)
        {
            env = e.NewTextValue;
           
        }

        private void Email_Completed(object sender, TextChangedEventArgs e)
        {
            email = e.NewTextValue;
        }

        private void Entry_Completed(object sender, TextChangedEventArgs e)
        {
            password = e.NewTextValue;
        }
    }
}
