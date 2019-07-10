using System;
using System.IO;
using System.Data.SQLite;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace yetAnotherMovieDB
{
    class Program
    {
        public static void Main()
        {
            //if statement to view or add is needed
            //Console.WriteLine("Do you want to view your database or add a new movie?");


            // If the user wants to add
            //Call the constructor that has no parameters.
           Movie movie = new Movie(); //hmm, not sure how to use
                                      /*Console.WriteLine("What is your movie name?");
                                       movie.Title = Console.ReadLine();
                                       Console.WriteLine("What is your movie type? DVD, Bluray, or Digital?");
                                       movie.MovieType = Console.ReadLine();
                                       //have to convert int to string since NumOfCopies is an int in the Movie class
                                       string movieCopies;
                                       Console.WriteLine("How many copies do you have?");
                                       movieCopies = Console.ReadLine();

                                       //convert movieCopies from string to int and return answer depending on number of copies owned
                                       // Create a list to write db objects to. will convert list objects to database objects and save to MovieDB 
                                       List<string> movieList = new List<string>();



                                       int copies = movie.NumOfCopies;
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
                                           Console.WriteLine(copies + " copy of " + movie.Title + " of type " + movie.MovieType + " has been added to your database.");
                                       }
                                       else
                                       {
                                           Console.WriteLine(copies + " copies of " + movie.Title + " of type " + movie.MovieType + " has been added to your database.");
                                       }

                                       // Add items using Add method   
                                       movieList.Add(movie.Title + ", " + movie.MovieType + ", " + copies.ToString());

                                       // Show items in list
                                       foreach (string movieItem in movieList)
                                       {
                                           Console.WriteLine(movieItem);
                                       } ERASE HERE*/

            //search entire database by title
            //path to MovieDB in cs variable
            string cs = "Data Source=C:\\Users\\Brittney\\Desktop\\db\\MovieDB";
            //string cs = @"Data Source=.\movieDB"; relative path needed

            Console.WriteLine("Enter any movie title to see if its in your movie database: ");
            movie.Title = Console.ReadLine();

            //connection to MovieDB 
            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();
                //user can search database without having to type quotes for title (string value)
                string stm = "SELECT * FROM MovieDB WHERE title = '" + movie.Title+ "'";
                
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
                                Console.WriteLine(rdr.GetName(i) + ": " + rdr.GetValue(i));
                            }
                            
                            //Console.WriteLine("You have {0} copy of {1} of type {2}.");
                        }
                    }
                }

                //close connection
                con.Close();


                //edit items in database by title
                //path to MovieDB in cs variable
                /*string cs1 = "Data Source=C:\\Users\\Brittney\\Desktop\\db\\MovieDB";

                Console.WriteLine("Which movie would you like to edit? ");
                movie.Title = Console.ReadLine();
                Console.WriteLine("Which movie would you like to edit? ");

                //connection to MovieDB 
                using (SqliteConnection con1 = new SqliteConnection(cs1))
                {
                    con.Open();
                    string stm1 = "UPDATE MovieDB WHERE title = " + movie.Title;

                    // execute select statement
                    using (SqliteCommand cmd1 = new SqliteCommand(stm1, con1))
                    {
                        using (SqliteDataReader rdr1 = cmd1.ExecuteReader())
                        {
                            while (rdr1.Read())
                            {
                                //will write if statement to only return records
                                Console.WriteLine("Here is your record: " + stm1);
                                //will write if statement showing this message only if movie is in database
                                Console.WriteLine(movie.Title + " is in your database.");
                            }
                        }
                    }

                    //close connection*/
                    //con.Close();


                }

            Console.ReadKey();


        }


    }
}
