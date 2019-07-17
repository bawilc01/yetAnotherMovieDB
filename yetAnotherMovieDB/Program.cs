using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;

namespace MovieApp
{
    class Program
    {
        static void Main()
        {
            //1  - Add
            //2  - View
            //3  - Search
            //4  - Edit
            //5  - Delete
            // anything else - Exit

            //need to update to relative connection string throughout app
            //SQLiteConnection MovieDatabaseConnection = new SQLiteConnection(@"Data Source = C:\Users\Brittney\source\repos\yetAnotherMovieDB\yetAnotherMovieDB\bin\Debug\netcoreapp2.1\MovieDatabase.sqlite; version=3;");

            Console.WriteLine("Welcome. Please make a selection.");
            Console.WriteLine("Press 1 to add a movie. Press 2 to view entire movie database. Press 3 to search a specific movie by title. Press 4 to update a movie's title, type or number of copies. Press 5 to delete a movie by title and type. Press any other key to quit.");

            var input = int.Parse(Console.ReadLine());

            switch (input)
            {
                case 1:
                    Movie newMovie = new Movie();
                    Console.WriteLine("What is the title of your movie?");
                    newMovie.Title = Console.ReadLine();


                    Console.WriteLine("What is your movie type? DVD, Bluray, or Digital?");
                    newMovie.MovieType = Console.ReadLine();

                    //will try to fix int not being written to db before deadline
                    //have to convert int to string since NumOfCopies is an int in the Movie class
                    //convert movieCopies from string to int and return answer depending on number of copies owned

                    //string movieCopies;
                    Console.WriteLine("How many copies do you have?");
                    newMovie.NumOfCopies = Console.ReadLine();

                    //will try to fix before deadline; int not being written to db
                    /*int copies = newMovie.NumOfCopies;
                    if (!Int32.TryParse(movieCopies, out copies)) 
                    if
                    {
                        Console.WriteLine("Invalid data input. Only whole numbers accepted. Please try again.");
                    }*/
                    if (newMovie.NumOfCopies == "0")
                    {
                        Console.WriteLine("Value cannot be 0. Please enter 1 or more copies.");
                    }
                    else if (newMovie.NumOfCopies == "1")
                    {
                        AddNewMovie(newMovie);
                        Console.WriteLine(newMovie.NumOfCopies + " copy of " + newMovie.Title + " of type " + newMovie.MovieType + " has been added to your database.");
                    }
                    else
                    {
                        AddNewMovie(newMovie);
                        Console.WriteLine(newMovie.NumOfCopies + " copies of " + newMovie.Title + " of type " + newMovie.MovieType + " has been added to your database.");
                    }
                    break;
            
                case 2:
                    Console.WriteLine("Here is your movie list: ");
                    GetMovies();
                    break;
                case 3: 
                    Console.WriteLine("What is the title of your movie?");
                    string movieTitleSearch = Console.ReadLine();
                    //works if I don't type with the right case or use a movie that doesn't exist
                    //if movie exists, it returns it but still writes "There are no movies with that title in your database." to the console
                    if (SearchMovies(movieTitleSearch).Count == 0)
                            {  
                                Console.WriteLine("There are no movies with that title in your database.");
                            }
                        else
                            {
                                SearchMovies(movieTitleSearch);
                            }
                    break;                    
                case 4:
                    //edit logic to be added on 7/17
                case 5:
                //delete logic to be added on 7/17
                //break;
                default:
                    Console.WriteLine("Exiting... valid input not selected.");
                    Environment.Exit(0);
                    return;

            }


        }
        /***********************
         * METHODS
         ***********************/

        /*
        VIEW ENTIRE MOVIE DATABASE METHOD 
        */
        private static List<Movie> GetMovies()
        {
            using (SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source = C:\Users\Brittney\source\repos\yetAnotherMovieDB\yetAnotherMovieDB\bin\Debug\netcoreapp2.1\MovieDatabase.sqlite; version=3;"))
            {
                m_dbConnection.Open();

                string stm = "SELECT * FROM MovieDatabase;";

                using (SQLiteCommand cmd = new SQLiteCommand(stm, m_dbConnection))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            for (int i = 0; i < rdr.FieldCount; i++)
                            {
                                //prints column name and result(s) to database
                                Console.WriteLine("{0} : {1} ", rdr.GetName(i).ToUpper(), rdr.GetValue(i));
                            }
                        }
                    }
                }

                m_dbConnection.Close();
            }
            return new List<Movie>();
        }
        /*
        SEARCH DATABASE FOR A SPECIFIC FILM 
        */
        private static List<Movie> SearchMovies(string movieTitle)
        
        {
            List<Movie> newMovieList = new List<Movie>();
            using (SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source = C:\Users\Brittney\source\repos\yetAnotherMovieDB\yetAnotherMovieDB\bin\Debug\netcoreapp2.1\MovieDatabase.sqlite; version=3;"))
            {
                m_dbConnection.Open();

                    string stm = "SELECT * FROM MovieDatabase WHERE title = '" + movieTitle.ToUpper() + "';";

                    using (SQLiteCommand cmd = new SQLiteCommand(stm, m_dbConnection))
                    {
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                for (int i = 0; i < rdr.FieldCount; i++)
                                {
                                    //prints column name and result(s) to database
                                    if (rdr.HasRows)
                                    {
                                        //Console.WriteLine(rdr.GetName(i) + ": " + rdr.GetValue(i));
                                        Console.WriteLine("{0} : {1} ", rdr.GetName(i).ToUpper(), rdr.GetValue(i));
                                    }
                                }
                            }
                        }
                    }

                m_dbConnection.Close();
            }
            return newMovieList;
        }
        /*
        ADD A MOVIE TO THE DATABASE
        */
        public static void AddNewMovie(Movie movie)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source = C:\Users\Brittney\source\repos\yetAnotherMovieDB\yetAnotherMovieDB\bin\Debug\netcoreapp2.1\MovieDatabase.sqlite; version=3;");
            m_dbConnection.Open();

            try
            {
                SQLiteCommand insertSQL = new SQLiteCommand("INSERT INTO MovieDatabase (title, movieType, numOfCopies) VALUES ('" + movie.Title.ToUpper() + "'" + ", '" + movie.MovieType.ToUpper() + "'" + ", " + movie.NumOfCopies.ToUpper() + ");", m_dbConnection);
                insertSQL.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            m_dbConnection.Close();
        }
    }




    
}