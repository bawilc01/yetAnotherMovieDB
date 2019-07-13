using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Linq;

namespace MovieApp
{
    public class Movie
    {
        public string Title { get; set; }
        public string MovieType { get; set; } //want to be enum
        public int NumOfCopies { get; set; } //want to be an int
    }

}
