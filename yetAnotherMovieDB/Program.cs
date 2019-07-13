using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp
{
    public class MainProgram
    {

        public static void Main()
        {
            /******************************************
             * CREATE TABLE, CREATE DB CONNECTION - does this need to be done every time?
             ******************************************/
            //SQLiteConnection.CreateFile("MovieDatabase.db");

            SQLiteConnection MovieDatabaseConnection = new SQLiteConnection(@"Data Source = C:\Users\Brittney\source\repos\yetAnotherMovieDB\yetAnotherMovieDB\bin\Debug\netcoreapp2.1\MovieDatabase.db; version=3;");

            MovieDatabaseConnection.Open();

            //string sql = "CREATE TABLE MovieDatabase (title VARCHAR(20), movieType VARCHAR(20), numOfCopies INT)";

            //SQLiteCommand command = new SQLiteCommand(sql, MovieDatabaseConnection);
            //command.ExecuteNonQuery();

            /******************************************
            * CREATE INSTANCE OF CLASS
            ******************************************/

            Movie newMovie = new Movie();


            /******************************************
             * ADD MOVIE TO DATABASE: NOT COMPLETE, 7/13/2019 
             ******************************************/
            /******************************************
            * A = Add
            * V = View
            * S = Search
            * D = Delete
            ******************************************/


            Console.WriteLine("Do you want to view your database or add a new movie? Press A for Add a Movie, V for View Movie Database, S for Search database, and D to Delete a database item, and Q to quit.");
            string userInput = Console.ReadLine();

            //  using (SQLiteConnection conAdd = new SQLiteConnection(MovieDatabaseConnection))

             while (userInput.ToUpper() == "A")
                {
                    Console.WriteLine("What is the title of your movie?");
                    newMovie.Title = Console.ReadLine();

                    Console.WriteLine("What is your movie type? DVD, Bluray, or Digital?");
                    newMovie.MovieType = Console.ReadLine();

                    //have to convert int to string since NumOfCopies is an int in the Movie class
                    //convert movieCopies from string to int and return answer depending on number of copies owned

                    string movieCopies;
                    Console.WriteLine("How many copies do you have?");
                    movieCopies = Console.ReadLine();


                    int copies = newMovie.NumOfCopies;
                    if (!Int32.TryParse(movieCopies, out copies))
                    {
                        Console.WriteLine("Invalid data input. Only whole numbers accepted. Please try again.");
                        Main(); //is calling Main the best way?
                                //want to continue the loop but continue doesn't work here
                    }
                    else if (copies == 0)
                    {
                        Console.WriteLine("You have to enter a number greater than 1.");
                        Main(); //is calling Main the best way?
                                //want to continue the loop but continue doesn't work here
                    }
                    else if (copies == 1)
                    {
                        string sql = "insert into MovieDatabase (title, movieType, numOfCopies) values ('" + newMovie.Title + "'" + ", '" + newMovie.MovieType + "'" + ", " + newMovie.NumOfCopies + ");";
                        SQLiteCommand command = new SQLiteCommand(sql, MovieDatabaseConnection);
                        command.ExecuteNonQuery();
                        Console.WriteLine(copies + " copy of " + newMovie.Title + " of type " + newMovie.MovieType + " has been added to your database.");
                    }
                    else
                    {
                        string sql = "insert into MovieDatabase (title, movieType, numOfCopies) values ('" + newMovie.Title + "'" + ", '" + newMovie.MovieType + "'" + ", " + newMovie.NumOfCopies + ");";
                        SQLiteCommand command = new SQLiteCommand(sql, MovieDatabaseConnection);
                        command.ExecuteNonQuery();
                        Console.WriteLine(copies + " copies of " + newMovie.Title + " of type " + newMovie.MovieType + " has been added to your database.");

                    }
                Console.WriteLine("Do you want to view your database or add a new movie? Press A for Add a Movie, V for View Movie Database, S for Search database, and D to Delete a database item, and Q to quit.");
                userInput = Console.ReadLine();

            }

             /******************************************
             * SEARCH DATABASE
             ******************************************/
                while (userInput.ToUpper() == "S")
                {
                    Console.WriteLine("Enter any movie title to see if its in your movie database: ");
                    newMovie.Title = Console.ReadLine();
                    using (SQLiteConnection con = new SQLiteConnection(MovieDatabaseConnection))
                    {
                        string stm = "SELECT * FROM MovieDatabase WHERE title = '" + newMovie.Title + "';";

                        using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                        {
                            using (SQLiteDataReader rdr = cmd.ExecuteReader())
                            {
                                while (rdr.Read())
                                {
                                    //will write if statement showing this message only if movie is in database
                                    //searching database for movie
                                    for (int i = 0; i < rdr.FieldCount; i++)
                                    {
                                        //prints column name and result(s) to database
                                        if (rdr.HasRows)
                                        {
                                            Console.WriteLine(newMovie.Title + " is in your database.");
                                            Console.WriteLine(rdr.GetName(i) + ": " + rdr.GetValue(i));
                                        }
                                        else
                                        {
                                            Console.WriteLine(newMovie.Title + " is not in your database.");
                                        }
                                    }
                                }
                            }
                        }
                    Console.WriteLine("Do you want to view your database or add a new movie? Press A for Add a Movie, V for View Movie Database, S for Search database, and D to Delete a database item, and Q to quit.");
                    userInput = Console.ReadLine();
                }

                    /******************************************
                    * SEARCH DATABASE
                    ******************************************/
                    while (userInput.ToUpper() == "V")
                    {
                        using (SQLiteConnection con = new SQLiteConnection(MovieDatabaseConnection))
                        {
                            string stm = "SELECT * FROM MovieDatabase;";

                            using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                            {
                                using (SQLiteDataReader rdr = cmd.ExecuteReader())
                                {
                                    while (rdr.Read())
                                    {
                                        //will write if statement showing this message only if movie is in database
                                        //searching database for movie
                                        for (int i = 0; i < rdr.FieldCount; i++)
                                        {
                                            //prints column name and result(s) to database
                                            if (rdr.HasRows)
                                            {
                                                Console.WriteLine(newMovie.Title + " is in your database.");
                                                Console.WriteLine(rdr.GetName(i) + ": " + rdr.GetValue(i));
                                            }
                                            else
                                            {
                                                Console.WriteLine(newMovie.Title + " is not in your database."); ;
                                            }
                                        }
                                    }
                                }
                            }
                        Console.WriteLine("Do you want to view your database or add a new movie? Press A for Add a Movie, V for View Movie Database, S for Search database, and D to Delete a database item, and Q to quit.");
                        userInput = Console.ReadLine();
                    }

                    if (userInput.ToUpper() == "Q")
                    {
                        Console.WriteLine("Exiting application");
                        MovieDatabaseConnection.Close();
                        Task.Delay(3000).Wait();
                        Environment.Exit(0);

                    }
                }
           
            }

        }
    }
}

/* 
 * 
 * try
{
    string sql = "select * from Condition";
SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

SQLiteDataReader reader = command.ExecuteReader();

    while (reader.Read())
        Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["id"]);

    Console.ReadLine();
    return null;
}
catch (Exception exc)
{
    return null;
}
finally
{
    m_dbConnection.Close();
}
*/