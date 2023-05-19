using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BlogSpot
{
    public partial class MainPage : ContentPage
    {
        private DatabaseHelper dbHelper;
        public MainPage()
        {
            InitializeComponent();
            string dbPath =
                Path.Combine(System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal), "myDatabase.db");

            dbHelper = new DatabaseHelper(dbPath);
        }

        private void Register_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegistrationPage());
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

            User user = dbHelper.GetUser(username, password);

            if(user == null)
            {
                DisplayAlert("Error", "Invalid username or password.", "OK");
                return;
            }

            DisplayAlert("Success", "Login Successful!", "OK");

            UsernameEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;

            Navigation.PushAsync(new DisplayPostActivity(user.UserID));
        }
    }
}
