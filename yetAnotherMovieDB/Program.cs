using System;
using System.IO;
using System.Data.SQLite;
using System.Linq;
using System.Collections.Generic;

namespace yetAnotherMovieDB
{
    class Program
    {
        public static void Main()
        {
            //Console.WriteLine("Do you want to view your database or add a new movie?");
            //if statement to view or add



            // Call the constructor that has no parameters.
            Movie movie = new Movie(); //hmm, not sure how to use
            Console.WriteLine("What is your movie name?");
            movie.Title = Console.ReadLine();
            Console.WriteLine("What is your movie type? DVD, Bluray, or Digital?");
            movie.MovieType = Console.ReadLine();
            //have to convert int to string since NumOfCopies is an int in the Movie class
            string movieCopies;
            Console.WriteLine("How many copies do you have?");
            movieCopies = Console.ReadLine();

            //convert movieCopies from string to int and return answer depending on number of copies owned
            // Create a list  
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
            }

            Console.ReadKey();


        }


    }
}
