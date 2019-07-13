using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Linq;

namespace MovieApp
{
    public class MainProgram
    {

        public static void Main()
        {
            /******************************************
             * CREATE TABLE, CREATE DB CONNECTION - does this need to be done every time?
             ******************************************/
            SQLiteConnection.CreateFile("MovieDatabase.sqlite");

            SQLiteConnection MovieDatabaseConnection = new SQLiteConnection("Data Source=MovieDatabase.sqlite;Version=3;");

            MovieDatabaseConnection.Open();

            string sql = "CREATE TABLE MovieDatabase (title VARCHAR(20), movieType VARCHAR(20), numOfCopies INT)";

            SQLiteCommand command = new SQLiteCommand(sql, MovieDatabaseConnection);
            command.ExecuteNonQuery();

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


            Console.WriteLine("Do you want to view your database or add a new movie? Press A for Add a Movie, V for View Movie Database, S for Search database, and D to Delete a database item.");
            string userInput = Console.ReadLine();

            using (SQLiteConnection conAdd = new SQLiteConnection(MovieDatabaseConnection))

                if (userInput.ToUpper() == "A")
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


                    //List<string> movieList = new List<string>();
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
                        sql = "insert into MovieDatabase (title, movieType, numOfCopies) values ('" + newMovie.Title + "'" + ", '" + newMovie.MovieType + "'" + ", " + newMovie.NumOfCopies + ")";
                        command = new SQLiteCommand(sql, MovieDatabaseConnection);
                        command.ExecuteNonQuery();
                        Console.WriteLine(copies + " copy of " + newMovie.Title + " of type " + newMovie.MovieType + " has been added to your database.");
                    }
                    else
                    {
                        sql = "insert into MovieDatabase (title, movieType, numOfCopies) values ('" + newMovie.Title + "'" + ", '" + newMovie.MovieType + "'" + ", " + newMovie.NumOfCopies + ")";
                        command = new SQLiteCommand(sql, MovieDatabaseConnection);
                        command.ExecuteNonQuery();
                        Console.WriteLine(copies + " copies of " + newMovie.Title + " of type " + newMovie.MovieType + " has been added to your database.");
                    }
                    conAdd.Close();
                    Environment.Exit(0);
                }
                /******************************************
                 * SEARCH DATABASE
                 ******************************************/
                else if (userInput.ToUpper() == "S")
                {
                    string cs = "Data Source=MovieDatabase.sqlite;Version=3;"; //TRYING TO FIND RELATIVE PATH

                    Console.WriteLine("Enter any movie title to see if its in your movie database: ");
                    newMovie.Title = Console.ReadLine();

                    //connection to MovieDatabase 
                    using (SQLiteConnection con = new SQLiteConnection(cs))
                    {
                        con.Open();
                        //user can search database without having to type quotes for title (string value)
                        string stm = "SELECT * FROM MovieDatabase WHERE title = '" + newMovie.Title + "'";

                        // execute select statement
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
                        //close connection
                        con.Close();
                    }
                }
                else if (userInput.ToUpper() == "V")
                {
                    string cs = "Data Source=MovieDatabase.sqlite;Version=3;"; //TRYING TO FIND RELATIVE PATH

                    /*Console.WriteLine("Enter any movie title to see if its in your movie database: ");
                    newMovie.Title = Console.ReadLine();*/

                    //connection to MovieDatabase 
                    using (SQLiteConnection con = new SQLiteConnection(cs))
                    {
                        con.Open();
                        //user can search database without having to type quotes for title (string value)
                        string stm = "SELECT * FROM MovieDatabase";

                        // execute select statement
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

                                        Console.WriteLine("Database Results: ");
                                        Console.WriteLine(rdr.GetName(i) + ": " + rdr.GetValue(i));

                                    }
                                }
                            }
                        }
                        //close connection
                        con.Close();

                    }
                }
                else
                {
                    Console.WriteLine("Exiting application.");
                    Console.ReadKey();
                    Environment.Exit(0);
                }


            /*Not sure if this is needed yet
            // Add items using Add method   
            movieList.Add(newMovie.Title + ", " + newMovie.MovieType + ", " + copies.ToString());
            // Show items in list
            foreach (string movieItem in movieList)
            {
                Console.WriteLine(movieItem);
            }


            */

        }
    }
}
