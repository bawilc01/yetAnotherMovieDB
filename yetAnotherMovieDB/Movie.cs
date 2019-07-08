using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Linq;

namespace yetAnotherMovieDB
{
    public class Movie
    {
        //public int Id { get; set; } not sure how to incorporate yet
        public string Title { get; set; }
        public string MovieType { get; set; } //want to be enum
        public int NumOfCopies { get; set; } //want to be an int
    }

}
