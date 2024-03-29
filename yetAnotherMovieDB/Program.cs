﻿using System;
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
            //6  - Exit
            /*
            CREATE TABLE MovieDatabase IF NOT EXISTS 
            */



            using (SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=MovieDatabase.sqlite;Version=3;"))
            {
                m_dbConnection.Open();
                try
                {
                    SQLiteCommand createTable = new SQLiteCommand("CREATE TABLE IF NOT EXISTS MovieDatabase (title STRING, movieType STRING, numofCopies STRING);", m_dbConnection);
                    createTable.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                m_dbConnection.Close();
            }




            //relative path to db file
            //SQLiteConnection MovieDatabaseConnection = new SQLiteConnection(@"Data Source = ~\yetAnotherMovieDB\yetAnotherMovieDB\bin\Debug\netcoreapp2.1\MovieDatabase.sqlite)
            var lines = new[] {
                "Press 1 to add a movie.",
                "Press 2 to view entire movie database.",
                "Press 3 to search a specific movie by title.",
                "Press 4 to update a movie's title, type or number of copies.",
                "Press 5 to delete a movie by title and type.",
                "Press 6 to quit."
            };

            
            string userInput = string.Empty;
            int parsedInt = 0;
            bool inputValid = false;


            while (!inputValid)
            {
                foreach (var line in lines)
                    Console.WriteLine(line);

                userInput = Console.ReadLine();
                inputValid = int.TryParse(userInput, out parsedInt);

                if (parsedInt < 1 || parsedInt > 6 || !inputValid)
                {
                    inputValid = false;
                    Console.WriteLine("Invalid input. Options are a number 1-6. Please try again.");
                    continue;
                }

                switch (parsedInt)
                    {
                        case 1:
                            Movie newMovie = new Movie();
                            Console.WriteLine("What is the title of your movie?");
                            newMovie.Title = Console.ReadLine().ToUpper();
                        

                            if (SearchMovies(newMovie.Title).Count() >= 1)
                            {
                                Console.WriteLine("This movie exists. Do you want to update the quantity? Enter Y or N.");
                                string answer = Console.ReadLine();

                                if (answer == "Y")
                                {
                                    //edit movie
                                    Console.WriteLine("What is the new number of copies?");
                                    newMovie.NumOfCopies = int.Parse(Console.ReadLine());
                                    EditMovieCopies(newMovie.Title, newMovie.NumOfCopies);
                                    Console.WriteLine("The number of copies for " + newMovie.Title + "is updated to " + newMovie.NumOfCopies + ".");
                                }
                                else
                                {
                                    Environment.Exit(0);
                                }

                            }
                            else
                            {
                                Console.WriteLine("What is your movie type? DVD, Bluray, or Digital?");
                                newMovie.MovieType = Console.ReadLine();

                                Console.WriteLine("How many copies do you have?");
                                newMovie.NumOfCopies = int.Parse(Console.ReadLine());

                                if (newMovie.NumOfCopies == 0)
                                {
                                    Console.WriteLine("Value cannot be 0. Please enter 1 or more copies.");
                                    Main();
                                
                                }

                                else if (newMovie.NumOfCopies == 1)
                                {
                                    AddNewMovie(newMovie);
                                    Console.WriteLine(newMovie.NumOfCopies + " copy of " + newMovie.Title + " of type " + newMovie.MovieType + " has been added to your database.");
                                    Main();
                                }
                                else
                                {
                                    AddNewMovie(newMovie);
                                    Console.WriteLine(newMovie.NumOfCopies + " copies of " + newMovie.Title + " of type " + newMovie.MovieType + " has been added to your database.");
                                    Main();
                                }
                            }
                        break;
                        case 2:
                            Console.WriteLine("Here is your movie list: ");
                            GetMovies();
                            Main();
                            break;
                        case 3:
                            Console.WriteLine("What is the title of your movie?");
                            string movieTitleSearch = Console.ReadLine();

                            SearchMovies(movieTitleSearch);
                            Main();
                            break;
                        case 4:
                            Console.WriteLine("To edit title, press 1. To edit movie type, press 2. To edit number of copies, press 3. To exit, press 4.");
                            var editInput = int.Parse(Console.ReadLine());
                            if (editInput == 1)
                            {
                                Console.WriteLine("What is the current title of your movie?");
                                string useroriginalTitle = Console.ReadLine();

                                Console.WriteLine("What is the new title?");
                                string userNewTitle = Console.ReadLine();

                                EditMovieTitle(useroriginalTitle, userNewTitle);
                                Console.WriteLine("Your movie title is updated.");
                            }
                            else if (editInput == 2)
                            {
                                Console.WriteLine("What is the title of the movie needing an updated type?");
                                string userMovieTitle = Console.ReadLine();

                                Console.WriteLine("What is the movie's current type?");
                                string userCurrentType = Console.ReadLine();

                                Console.WriteLine("What is the movie's new type?");
                                string userNewType = Console.ReadLine();

                                EditMovieType(userMovieTitle, userNewType);
                                Console.WriteLine("Your movie '" + userMovieTitle + "' is updated from type '" + userCurrentType + "' to type '" + userNewType + "'.");
                            }
                            else if (editInput == 3)
                            {
                                Console.WriteLine("What is the title of the movie needing an updated number of copies?");
                                string userMovieTitle = Console.ReadLine();

                                //string stringNum;
                                Console.WriteLine("What is the movie's current number of copies?");
                                int userCurrentCopies = int.Parse(Console.ReadLine());

                                Console.WriteLine("What is the movie's new number of copies?");
                                int userUpdatedNumofCopies = int.Parse(Console.ReadLine());

                                Console.WriteLine("Your number of copies for '" + userMovieTitle + "' is updated from " + userCurrentCopies + " to '" + userUpdatedNumofCopies + "'.");
                                EditMovieCopies(userMovieTitle, userUpdatedNumofCopies);

                            }
                            else if (editInput == 4) //will fix to not accept strings with while loop and tryParse
                            {
                                Console.WriteLine("No edits requested. Exiting.");
                                Environment.Exit(0);
                            }
                            
                            Main();
                            break;
                        case 5:
                            Console.WriteLine("What is the title of your movie?");
                            string movieToBeDeleted = Console.ReadLine();
                            SearchMovies(movieToBeDeleted);

                            Console.WriteLine("Is this the movie you want to delete? Press 1 for Yes or 2 for No.");
                            var yesOrNo = int.Parse(Console.ReadLine());
                            if (yesOrNo == 1)
                            {
                                DeleteMovies(movieToBeDeleted);
                                Console.WriteLine("Your movie '" + movieToBeDeleted + "' has been deleted from your database.");
                            }
                            else if (yesOrNo == 2)
                            {
                                Console.WriteLine("Your movie '" + movieToBeDeleted + "' will not be deleted from your database.");
                            }
                            else
                            {
                                Console.WriteLine("Valid option not selected. Exiting.");
                            }
                            
                            Main();
                            break;
                        case 6:
                            Console.WriteLine("Exiting.");
                            Environment.Exit(0);
                            break;
                        default:
                            return;

                    }
            }
        }
        /***********************
         * METHODS - will update numOfCopies from string to Int
         ***********************/

        /*
        VIEW ENTIRE MOVIE DATABASE METHOD 
        */
        private static List<Movie> GetMovies()
        {
            using (SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=MovieDatabase.sqlite;Version=3;"))
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
                                Console.WriteLine("{0}: {1} ", rdr.GetName(i).ToUpper(), rdr.GetValue(i));
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

            using (SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=MovieDatabase.sqlite;Version=3;"))
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
        private static void AddNewMovie(Movie movie)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=MovieDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            try
            {
                SQLiteCommand insertSQL = new SQLiteCommand("INSERT INTO MovieDatabase (title, movieType, numOfCopies) VALUES ('" + movie.Title.ToUpper() + "'" + ", '" + movie.MovieType.ToUpper() + "'" + ", " + movie.NumOfCopies + ");", m_dbConnection);
                insertSQL.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            m_dbConnection.Close();
        }
        /*
        EDIT A MOVIE TITLE IN THE DATABASE
        */
        private static List<Movie> EditMovieTitle(string originalTitle, string titleToEdit)
        {
            List<Movie> newMovieList = new List<Movie>();

            using (SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=MovieDatabase.sqlite;Version=3;"))
            {
                m_dbConnection.Open();
                try
                {
                    //string stm = "UPDATE MovieDatabase SET title = '" + titleToEdit.ToUpper() + "' WHERE title= '" + originalTitle + "';";
                    SQLiteCommand updateSQL = new SQLiteCommand("UPDATE MovieDatabase SET title = '" + titleToEdit.ToUpper() + "' WHERE title= '" + originalTitle.ToUpper() + "'OR title= '" + originalTitle + "';", m_dbConnection);
                    updateSQL.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                m_dbConnection.Close();
            }
            return newMovieList;
        }

        /*
        EDIT A MOVIE TYPE IN THE DATABASE
        */
        private static List<Movie> EditMovieType(string movieTitle, string newType)
        {
            List<Movie> newMovieList = new List<Movie>();

            using (SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=MovieDatabase.sqlite;Version=3;"))
            {
                m_dbConnection.Open();
                try
                {
                    SQLiteCommand updateSQL = new SQLiteCommand("UPDATE MovieDatabase SET movieType = '" + newType.ToUpper() + "' WHERE title= '" + movieTitle.ToUpper() + "'OR title= '" + movieTitle + "';", m_dbConnection);
                    updateSQL.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                m_dbConnection.Close();
            }
            return newMovieList;
        }

        // System.Convert.ToInt32("5");

        /*
        EDIT MOVIE COPIES AMOUNT IN THE DATABASE
        */
        private static List<Movie> EditMovieCopies(string movieTitle, int updateCopies)
        {
            List<Movie> newMovieList = new List<Movie>();

            using (SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=MovieDatabase.sqlite;Version=3;"))
            {
                m_dbConnection.Open();
                try
                {
                    SQLiteCommand updateSQL = new SQLiteCommand("UPDATE MovieDatabase SET numOfCopies = '" + updateCopies + "' WHERE title= '" + movieTitle.ToUpper() + "';", m_dbConnection);
                    updateSQL.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                m_dbConnection.Close();
            }
            return newMovieList;
        }

        /*
        DELETE MOVIE FROM DATABASE
        */
        private static List<Movie> DeleteMovies(string movieTitle)
        {
            List<Movie> newMovieList = new List<Movie>();

            using (SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=MovieDatabase.sqlite;Version=3;"))
            {
                m_dbConnection.Open();
                try
                {
                    SQLiteCommand deleteSQL = new SQLiteCommand("DELETE FROM MovieDatabase WHERE title= '" + movieTitle.ToUpper() + "' OR title = '" + movieTitle + "';", m_dbConnection);
                    deleteSQL.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                m_dbConnection.Close();
            }
            return newMovieList;
        }
    }
}