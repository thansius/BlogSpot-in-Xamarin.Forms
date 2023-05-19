using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlogSpot
{
    //nawawala ang emulator ko
    //while waiting sa emulator ko gawin muna natin database helper
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {

        private DatabaseHelper dbHelper;

        public RegistrationPage()
        {
            InitializeComponent();
            string dbPath =
                Path.Combine(System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal), "myDatabase.db");

            dbHelper = new DatabaseHelper(dbPath);


        }

        private void Register_Clicked(object sender, EventArgs e)
        {
            string fullName = FullNameEntry.Text;
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;
            string cpassword = ConfirmPasswordEntry.Text;

            //validations
            if(string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(cpassword))
            {
                DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

            if (!password.Equals(cpassword))
            {
                DisplayAlert("Error", "Passwords do not match", "OK");
                return;
            }

            if (dbHelper.IsUsernameExists(username))
            {
                DisplayAlert("Error", "Username already exists", "OK");
                return;
            }

            User user = new User(fullName, username, password);
            dbHelper.InsertUser(user);

            DisplayAlert("Success", "Registration Successful!", "OK");

            FullNameEntry.Text = string.Empty;
            UsernameEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
            ConfirmPasswordEntry.Text = string.Empty;

            Navigation.PushAsync(new MainPage());
            //bakit ayaw ng emulator ko


        }
    }
}