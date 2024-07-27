using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace CumulativeP1_n01691175.Models
{
    public class SchoolDbContext
    {
        //Blog database properties
        private static string User { get { return "root"; } }

        private static string Password { get { return "root"; } }

        private static string Database { get { return "schooldb"; } }

        private static string Server { get { return "localhost"; } }

        private static string Port { get { return "3306"; } }

        //Credentials to database connection
        protected static string ConnectionString
        {
            get
            {

                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password
                    + "; convert zero datetime = True";

            }
        }

        /// <summary>
        /// Returns connection to blog database
        /// </summary>
        /// <example>
        /// MySqlConnection Connection = School.AccessDatabase();
        /// </example>
        /// <returns>
        /// A mySqlConnection Object
        /// </returns>
        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
 
}