using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using ESC_Assessment.Models;
using static ESC_Assessment.Controllers.ESCController;

namespace ESC_Assessment.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ESCController : Controller
    {
        #region Full Data
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
        #endregion

        #region Get data by ID
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
            if (ret.Length > 0)
                ldm = JsonSerializer.Deserialize<List<Employee>>(ret) ?? new List<Employee>();
            return ldm;
        }

        [HttpGet("GetDependent")]
        public List<Dependent> GetDependent(int depID)
        {
            List<Dependent> ldm = new List<Dependent>();
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select dependent_id AS ID, first_name AS FirstName, last_name AS LastName, relationship as Relationship, employee_id as EmployeeID from dependents where dependent_id = '" + depID + "' FOR JSON PATH";
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
                ldm = JsonSerializer.Deserialize<List<Dependent>>(ret) ?? new List<Dependent>();
            return ldm;
        }

        [HttpGet("GetDepartment")]
        public List<Department> GetDepartment(int depID)
        {
            List<Department> ldm = new List<Department>();
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select department_id AS ID, department_name AS Name, location_id AS LocationID from departments where department_id = '" + depID + "' FOR JSON PATH";
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
                ldm = JsonSerializer.Deserialize<List<Department>>(ret) ?? new List<Department>();
            return ldm;
        }

        [HttpGet("GetJob")]
        public List<Job> GetJobs(int jobID)
        {
            List<Job> ldm = new List<Job>();
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select job_ID AS ID, job_title AS Title, min_salary AS MinSalary, max_salary as MaxSalary from jobs where job_id = '" + jobID + "' FOR JSON PATH";
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
                ldm = JsonSerializer.Deserialize<List<Job>>(ret) ?? new List<Job>();
            return ldm;
        }

        [HttpGet("GetLocation")]
        public List<Location> GetLocation(int locID)
        {
            List<Location> ldm = new List<Location>();
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select location_id AS ID, street_address AS StreetAddress, postal_code AS PostalCode, city as City, state_province as StateProvince, country_id as CountryID from locations where location_id = '" + locID + "' FOR JSON PATH";
            con.Open();
            string ret = "";
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    ret += rdr[0].ToString();
                }
            }
            if(ret.Length > 0)
                ldm = JsonSerializer.Deserialize<List<Location>>(ret) ?? new List<Location>();
            return ldm;
        }

        [HttpGet("GetCountry")]
        public List<Country> GetCountry(string countryID)
        {
            List<Country> ldm = new List<Country>();
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select country_id AS ID, country_name AS Name, region_id AS RegionID from countries where country_id = '" + countryID + "' FOR JSON PATH";
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
                ldm = JsonSerializer.Deserialize<List<Country>>(ret) ?? new List<Country>();
            return ldm;
        }

        [HttpGet("GetRegion")]
        public List<Region> GetRegion(int regionID)
        {
            List<Region> ldm = new List<Region>();
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select region_id AS ID, Region_name AS Name from regions where region_id = '" + regionID + "' FOR JSON PATH";
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
                ldm = JsonSerializer.Deserialize<List<Region>>(ret) ?? new List<Region>();
            return ldm;
        }

        #endregion

        #region DDL data

        [HttpGet("GetEmployees")]
        public List<ddlClass> GetEmployees()
        {
            List<ddlClass> ldm = new List<ddlClass>();
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select first_name + ' ' + last_name as Display, employee_id as ID from employees FOR JSON PATH";
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
        public List<ddlClassSpecial> GetCountries()
        {
            List<ddlClassSpecial> ldm = new List<ddlClassSpecial>();
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
            ldm = JsonSerializer.Deserialize<List<ddlClassSpecial>>(ret) ?? new List<ddlClassSpecial>();
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

        #region CRUD

        #endregion
    }
}
