using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using ESC_Assessment.Models;

namespace ESC_Assessment.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ESCController : Controller
    {

        [HttpGet("GetAllData")]
        public List<Employees> GetFullData()
        {
            List<Employees> ldm = new List<Employees>();
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_GetFull";
            cmd.Parameters.AddWithValue("@getType", 1);
            con.Open();
            string ret = "";
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    ret += rdr[0].ToString();
                }
            }
            ldm = JsonSerializer.Deserialize<List<Employees>>(ret) ?? new List<Employees>();
            return ldm;
        }
        [HttpGet("GetPartialData")]
        public List<Employees> GetData(int? employeeID = null, string fName = "", string lName = "", string email = "", string depName = "", string countyName = "", string regName = "")
        {
            List<Employees> ldm = new List<Employees>();
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_GetFull";
            cmd.Parameters.AddWithValue("@getType", 0);
            cmd.Parameters.AddWithValue("@employeeID", employeeID);
            cmd.Parameters.AddWithValue("@fName", fName);
            cmd.Parameters.AddWithValue("@lName", lName);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@dName", depName);
            cmd.Parameters.AddWithValue("@cName", countyName);
            cmd.Parameters.AddWithValue("@rName", regName);
            con.Open();
            string ret = "";
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    ret += rdr[0].ToString();
                }
            }
            if (ret.Length > 0)
            {
                ldm = JsonSerializer.Deserialize<List<Employees>>(ret) ?? new List<Employees>();
            }
            return ldm;
        }

        [HttpGet("GetEmployee")]
        public List<Employee> GetEmployee(int empID)
        {
            List<Employee> ldm = new List<Employee>();
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select employee_id AS ID, first_name AS FirstName, last_name AS LastName, email AS Email, phone_number AS PhoneNumber, hire_date AS HireDate, job_id AS JobID, salary AS Salary, department_id AS DepartmentID from employees where employee_id = '" + empID +"' FOR JSON PATH";
            con.Open();
            string ret = "";
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    ret += rdr[0].ToString();
                }
            }
            ldm = JsonSerializer.Deserialize<List<Employee>>(ret) ?? new List<Employee>();
            return ldm;
        }

        #region DDL data

        [HttpGet("GetEmployees")]
        public List<ddlClass> GetEmployees()
        {
            List<ddlClass> ldm = new List<ddlClass>();
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select first_name + ' ' last_name as Display, employee_id as ID from employees FOR JSON PATH";
            con.Open();
            string ret = "";
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    ret += rdr[0].ToString();
                }
            }
            ldm = JsonSerializer.Deserialize<List<ddlClass>>(ret) ?? new List<ddlClass>();
            return ldm;
        }

        [HttpGet("GetDependents")]
        public List<ddlClass> GetDependents()
        {
            List<ddlClass> ldm = new List<ddlClass>();
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select first_name + ' ' + last_name as Display, dependent_id as ID from jobs FOR JSON PATH";
            con.Open();
            string ret = "";
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    ret += rdr[0].ToString();
                }
            }
            ldm = JsonSerializer.Deserialize<List<ddlClass>>(ret) ?? new List<ddlClass>();
            return ldm;
        }

        [HttpGet("GetDepartments")]
        public List<ddlClass> GetDepartmnts()
        {
            List<ddlClass> ldm = new List<ddlClass>();
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select department_name as Display, department_id as ID from departments FOR JSON PATH";
            con.Open();
            string ret = "";
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    ret += rdr[0].ToString();
                }
            }
            ldm = JsonSerializer.Deserialize<List<ddlClass>>(ret) ?? new List<ddlClass>();
            return ldm;
        }

        [HttpGet("GetJobs")]
        public List<ddlClass> GetJobs()
        {
            List<ddlClass> ldm = new List<ddlClass>();
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select job_title as Display, job_id as ID from jobs FOR JSON PATH";
            con.Open();
            string ret = "";
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    ret += rdr[0].ToString();
                }
            }
            ldm = JsonSerializer.Deserialize<List<ddlClass>>(ret) ?? new List<ddlClass>();
            return ldm;
        }

        [HttpGet("GetCountries")]
        public List<ddlClass> GetCountries()
        {
            List<ddlClass> ldm = new List<ddlClass>();
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select country_name as Display, country_id as ID from countries FOR JSON PATH";
            con.Open();
            string ret = "";
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    ret += rdr[0].ToString();
                }
            }
            ldm = JsonSerializer.Deserialize<List<ddlClass>>(ret) ?? new List<ddlClass>();
            return ldm;
        }


        [HttpGet("GetRegions")]
        public List<ddlClass> GetRegions()
        {
            List<ddlClass> ldm = new List<ddlClass>();
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select region_name as Display, region_id as ID from regions FOR JSON PATH";
            con.Open();
            string ret = "";
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    ret += rdr[0].ToString();
                }
            }
            ldm = JsonSerializer.Deserialize<List<ddlClass>>(ret) ?? new List<ddlClass>();
            return ldm;
        }


        [HttpGet("GetLocations")]
        public List<ddlClass> GetLocations()
        {
            List<ddlClass> ldm = new List<ddlClass>();
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select street_address as Display, location_id as ID from jobs FOR JSON PATH";
            con.Open();
            string ret = "";
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    ret += rdr[0].ToString();
                }
            }
            ldm = JsonSerializer.Deserialize<List<ddlClass>>(ret) ?? new List<ddlClass>();
            return ldm;
        }

        #endregion
    }
}
