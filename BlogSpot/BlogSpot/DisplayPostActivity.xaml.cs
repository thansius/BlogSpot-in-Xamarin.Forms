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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisplayPostActivity : ContentPage
    {
        private DatabaseHelper dbHelper;
        private int userID;

        public DisplayPostActivity(int userID)
        {
            InitializeComponent();
            string dbPath =
                Path.Combine(System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal), "myDatabase.db");

            dbHelper = new DatabaseHelper(dbPath);

            this.userID = userID;

        }

        private void CreatePostButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreatePostActivity(userID));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //this will retrieve the posts for the current user
            var posts = dbHelper.GetPostsByUserID(userID);

            PostsListView.ItemsSource = posts;
        }

        private void PostsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem == null)
            {
                return;
            }

            Post selectedPost = (Post)e.SelectedItem;

            DisplayAlert("Post Details",
                $"Title: {selectedPost.PostTitle}\n" +
                $"Body: {selectedPost.PostBody}", "OK");

            PostsListView.SelectedItem = null;
        }
    }
}