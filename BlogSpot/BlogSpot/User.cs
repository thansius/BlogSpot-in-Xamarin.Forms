using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogSpot
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserID { get; set; }

        public string FullName { get; set; }

        [Unique]
        public string Username { get; set; }

        public string Password { get; set; }

        public User() { }

        public User(string fullName, string username, string password)
        {
            FullName = fullName;
            Username = username;
            Password = password;
        }
    }
}
