using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogSpot
{
    public class Post
    {
        [PrimaryKey, AutoIncrement]
        public int PostID { get; set; }

        public string PostTitle { get; set; }

        public string PostBody { get; set; }

        public DateTime PostDate { get; set; }

        [Indexed]
        public int UserID { get; set; }

    }
}
