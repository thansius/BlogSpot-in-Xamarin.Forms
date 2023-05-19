using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogSpot
{
    public class DatabaseHelper
    {
        private SQLiteConnection _database;

        public DatabaseHelper(string dbPath)
        {
            _database = new SQLiteConnection(dbPath);
            _database.CreateTable<User>();
            _database.CreateTable<Post>();
        }

        public void InsertUser(User user)
        {
            _database.Insert(user);
        }

        public User GetUser(string username, string password)
        {
            return _database.Table<User>().
                FirstOrDefault(x => x.Username == username && x.Password == password);
        }

        public bool IsUsernameExists(string username)
        {
            //unique dapat ang username natin so check muna kung wala na duplicate before insert
            return _database.Table<User>().Any(x => x.Username == username);
        }

        public void InsertPost(Post post)
        {
            _database.Insert(post);
        }

        public List<Post> GetAllPosts()
        {
            return _database.Table<Post>().ToList();
        }

        public List<Post> GetPostsByUserID(int userId)
        {
            return _database.Table<Post>().Where(p => p.UserID == userId).ToList();
        }


    }
}
