using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlogSpot
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatePostActivity : ContentPage
    {
        private DatabaseHelper dbHelper;
        private int userID;
        public CreatePostActivity(int userID)
        {
            InitializeComponent();
            string dbPath =
                Path.Combine(System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal), "myDatabase.db");

            dbHelper = new DatabaseHelper(dbPath);

            this.userID = userID;
        }

        private void Create_Clicked(object sender, EventArgs e)
        {
            string postTitle = PostTitleEntry.Text;
            string postBody = PostBodyEditor.Text;

            if(string.IsNullOrEmpty(postTitle) || string.IsNullOrEmpty(postBody))
            {
                DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

            Post newPost = new Post
            {
                PostTitle = postTitle,
                PostBody = postBody,
                PostDate = DateTime.Now,
                UserID = userID
            };

            dbHelper.InsertPost(newPost);

            DisplayAlert("Success", "Post Created Successfully!", "OK");

            PostTitleEntry.Text = string.Empty;
            PostBodyEditor.Text = string.Empty;

            Navigation.PushAsync(new DisplayPostActivity(userID));
        }
    }
}