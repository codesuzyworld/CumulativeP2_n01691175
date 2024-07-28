using CumulativeP1_n01691175.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using System.Web.Http.Cors;


namespace CumulativeP1_n01691175.Controllers
{
    public class TeacherDataController : ApiController
    {
        // Access the MySQL Database using database context class
        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// Returns a list of teachers
        /// </summary>
        /// <example>
        /// INPUT: https://localhost:44386/api/TeacherData/ListTeachers/
        /// OUTPUT:  All list of teachers in the format of: {EmployeeNumber} {HireDate} {Salary} {TeacherFname} {TeacherId} {TeacherLname} 
        /// </example> 
        /// <param name="SearchKey">The search term the user inputs</param>
        /// <returns></returns>

        // Access the teachers table for information about teachers 

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            // Create an instance of connection
            MySqlConnection Connection = School.AccessDatabase();

            // Open connection bt server and db
            Connection.Open();

            // Make new query for database 
            MySqlCommand cmd = Connection.CreateCommand();

            // The actual SQL query
            cmd.CommandText = "SELECT * FROM `teachers` WHERE lower(teacherfname) LIKE lower(@key) OR lower(teacherlname) LIKE lower(@key) OR lower(CONCAT(teacherfname, ' ', teacherlname)) LIKE lower(@key);";
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            // Gather Result Set of query into variable {ResultSet} 
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Create an empty list of teachers
            List<Teacher> Teachers = new List<Teacher>{};

            // Loop through each row to show the full result set
            while (ResultSet.Read())
            {
                // Correspond each variable in the model to the database columns
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                string HireDate = ResultSet["hiredate"].ToString();
                double Salary = Convert.ToDouble(ResultSet["salary"]);

                //Turn the column information into an index NewTeacher
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname= TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                //Add The New Teacher object to the list
                Teachers.Add(NewTeacher);
            }

            //Close connection between db and server
            Connection.Close();

            //Return the whole list of teacher information! 
            return Teachers;

        }

        /// <summary>
        /// Access the selected teacher from the database according to teacherID.
        /// Recieves an ID, and shows the corresponding teacher's data
        /// </summary>
        /// <param name="TeacherId">The ID of the teacher</param>
        /// <example>
        /// INPUT: https://localhost:44386/api/TeacherData/FindTeacher/{TeacherId}
        /// OUTPUT: {EmployeeNumber} {HireDate} {Salary} {TeacherFname} {TeacherId} {TeacherLname} 
        /// </example>
        /// <returns>The Teacher Object</returns>

        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{TeacherId}")]

        public Teacher FindTeacher(int TeacherId)
        {
            

            //Open connection to the database
            MySqlConnection Connection = School.AccessDatabase();
            Connection.Open();

            // Make new query for database 
            MySqlCommand Command = Connection.CreateCommand();
            
            //The SQL query here
            string query = "SELECT * from teachers WHERE teacherid = @teacherid";

            Command.Parameters.AddWithValue("@teacherid", TeacherId);
            Command.Prepare();
            Command.CommandText = query;

            MySqlDataReader ResultSet = Command.ExecuteReader();

            Teacher SelectedTeacher = new Teacher(); 

            //Run query and put the info into the selected teacher info object
            while (ResultSet.Read())
            {
                SelectedTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                SelectedTeacher.TeacherFname = ResultSet["teacherfname"].ToString();
                SelectedTeacher.TeacherLname = ResultSet["teacherlname"].ToString();
                SelectedTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
                SelectedTeacher.HireDate = ResultSet["hiredate"].ToString();
                SelectedTeacher.Salary = Convert.ToDouble(ResultSet["salary"]);
            }


            Connection.Close();

            return SelectedTeacher;
        }

        /// <summary>
        /// Deletes an Author from the connected MySQL Database if the ID of that author exists.
        /// </summary>
        /// <param name="id">The ID of the author.</param>
        /// <example>POST /api/TeacherData/DeleteTeacher/3</example>

        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Open connection to the database
            MySqlConnection Connection = School.AccessDatabase();
            Connection.Open();

            // Make new query for database 
            MySqlCommand Command = Connection.CreateCommand();

            //SQL query here!
            Command.CommandText = "Delete from teachers where teacherid=@id";
            Command.Parameters.AddWithValue("@id", id);
            Command.Prepare();

            Command.ExecuteNonQuery();
            Connection.Close();
        }

        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            //Open connection to the database
            MySqlConnection Connection = School.AccessDatabase();
            Connection.Open();

            // Make new query for database 
            MySqlCommand Command = Connection.CreateCommand();

            //SQL query here!
            string query = "INSERT INTO teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) values (@TeacherFname,@TeacherLname, @EmployeeNumber, CURRENT_DATE(), @Salary)";
            Command.CommandText = query;

            Command.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            Command.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            Command.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            Command.Parameters.AddWithValue("@Salary", NewTeacher.Salary);

            Command.Prepare();

            Command.ExecuteNonQuery();

            Connection.Close();
        }
    }
}
