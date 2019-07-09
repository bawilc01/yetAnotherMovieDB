/*using System;
using System.Text;
using System.Data;
using System.Data.SQLite;
using Mono.Data.Sqlite;

namespace yetAnotherMovieDB
/*{
    class DataClass
   {
        //path to MovieDB in cs variable
        string cs = "Data Source=C:\\Users\\Brittney\\Desktop\\db\\MovieDB";

        //connection to MovieDB 
        using (SqliteConnection con = new SqliteConnection(cs))
        {
            con.Open();
            string stm = "SELECT * FROM MovieDB WHERE title = " + movie.Title;
            
            
        }

        

        
        public DataTable selectQuery(string query)
        {
            SQLiteDataAdapter ad;
            DataTable dt = new DataTable();

            try
            {
                SQLiteCommand cmd;
                sqlite.Open();  //Initiate connection to the db
                cmd = sqlite.CreateCommand();
                cmd.CommandText = query;  //set the passed query
                ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dt); //fill the datasource
            }
            catch (SQLiteException ex)
            {
                //Add your exception code here.
            }
            sqlite.Close();
            return dt;
        }
    }*/