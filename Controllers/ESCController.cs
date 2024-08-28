using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using ESC_Assessment.Models;
using static ESC_Assessment.Controllers.ESCController;
using System.Diagnostics.Metrics;

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
            cmd.CommandText = "Select first_name + ' ' + last_name as Display, dependent_id as ID from dependents FOR JSON PATH";
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
            cmd.CommandText = "Select street_address as Display, location_id as ID from locations FOR JSON PATH";
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

        //----------------------------------------------
        //Creates
        [HttpPost("CreateEmployee")]
        public int CreateEmployee([FromBody]Employee employee)
        {
            //Manager ID is intentionally being left out since I don't have the business rules for managers.
            //I would add a boolean to the JOBS table to indicate if it's a managerial position
            int ret = -1;
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO EMPLOYEES(first_name, last_name, email, phone_number, hire_date, job_id, salary, department_id) VALUES('" + employee.FirstName + "', '" + employee.LastName + "', '" + employee.Email + "', '" + employee.PhoneNumber + "', '" + employee.HireDate + "', '" + employee.JobID + "', '" + employee.Salary + "', '" + employee.DepartmentID + "')";
            con.Open();
            ret = cmd.ExecuteNonQuery();
            return ret;
        }
        [HttpPost("CreateDependent")]
        public int CreateDependent([FromBody]Dependent dependent)
        {
            int ret = -1;
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO DEPENDENTS(first_name, last_name, relationship, employee_id) VALUES('" + dependent.FirstName + "', '" + dependent.LastName + "', '" + dependent.Relationship + "', '" + dependent.EmployeeID + "')";
            con.Open();
            ret = cmd.ExecuteNonQuery();
            return ret;
        }
        [HttpPost("CreateLocation")]
        public int CreateLocation([FromBody]Location location)
        {
            int ret = -1;
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO LOCATIONS(street_address, postal_code, city, state_province, country_id) VALUES('" + location.StreetAddress + "', '" + location.PostalCode + "', '" + location.City + "', '" + location.StateProvince + "', '" + location.CountryID + "')";
            con.Open();
            ret = cmd.ExecuteNonQuery();
            return ret;
        }
        [HttpPost("CreateCountry")]
        public int CreateCountry([FromBody]Country country)
        {
            //Country will not be able to be inserted into as getting a full list of country abreviations for ID is beyond the scope I think.  
            //I would change the table to have a proper ID column and change the current id to abreviation
            int ret = -1;
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO COUNTRIES(country_id, country_name, region_id) VALUES('" + country.Name + "', '" + country.RegionID + "')";
            con.Open();
            ret = cmd.ExecuteNonQuery();
            return ret;
        }
        [HttpPost("CreateRegion")]
        public int CreateRegion([FromBody]Region region)
        {
            int ret = -1;
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO REGIONS(region_name) VALUES('" + region.Name + "')";
            con.Open();
            ret = cmd.ExecuteNonQuery();
            return ret;
        }
        [HttpPost("CreateDepartment")]
        public int CreateDepartment([FromBody]Department department)
        {
            int ret = -1;
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO DEPARTMENTS(department_name, location_id) VALUES('" + department.Name + "', '" + department.LocationID + "')";
            con.Open();
            ret = cmd.ExecuteNonQuery();
            return ret;
        }
        [HttpPost("CreateJob")]
        public int CreateJob([FromBody]Job job)
        {
            int ret = -1;
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO JOBS(job_title, min_salary, max_salary) VALUES('" + job.Title + "', '" + job.MinSalary + "', '" + job.MaxSalary + "')";
            con.Open();
            ret = cmd.ExecuteNonQuery();
            return ret;
        }

        //--------------------------------------------
        //Updates
        [HttpPost("UpdateEmployee")]
        public int UpdateEmployee([FromBody]Employee employee)
        {
            int ret = -1;
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @"UPDATE EMPLOYEES
                                SET
	                                first_name = '" + employee.FirstName + "', " + 
	                                "last_name = '" + employee.LastName + "', " +
                                    "email = '" + employee.Email + "', " + 
	                                "phone_number = '" + employee.PhoneNumber + "', " +
                                    "hire_date = '" + employee.HireDate + "', " +
                                    "job_id = '" + employee.JobID + "', " +
                                    "salary = '" + employee.Salary + "', " +
                                    "department_id = '" + employee.DepartmentID + "' " +
                                "WHERE employee_id = '" + employee.ID + "'";
            con.Open();
            ret = cmd.ExecuteNonQuery();
            return ret;
        }
        [HttpPost("UpdateDependent")]
        public int UpdateDependent([FromBody]Dependent dependent)
        {
            int ret = -1;
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @"UPDATE DEPENDENTS
                                SET
	                                first_name = '" + dependent.FirstName + "', " +
                                    "last_name = '" + dependent.LastName + "', " +
                                    "relationship = '" + dependent.Relationship + "', " +
                                    "employee_id = '" + dependent.EmployeeID + "' " +
                                "WHERE dependent_id = '" + dependent.ID + "'";
            con.Open();
            ret = cmd.ExecuteNonQuery();
            return ret;
        }
        [HttpPost("UpdateLocation")]
        public int UpdateLocation([FromBody]Location location)
        {
            int ret = -1;
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @"UPDATE LOCATIONS
                                SET
	                                street_address = '" + location.StreetAddress + "', " +
                                    "postal_code = '" + location.PostalCode + "', " +
                                    "city = '" + location.City + "', " +
                                    "state_province = '" + location.StateProvince + "', " +
                                    "country_id = '" + location.CountryID + "' " +
                                "WHERE location_id = '" + location.ID + "'";
            con.Open();
            ret = cmd.ExecuteNonQuery();
            return ret;
        }
        [HttpPost("UpdateCountry")]
        public int UpdateCountry([FromBody]Country country)
        {
            int ret = -1;
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @"UPDATE COUNTRIES
                                SET
	                                country_name = '" + country.Name + "', " +
                                    "region_id = '" + country.RegionID + "' " +
                                "WHERE country_id = '" + country.ID + "'";
            con.Open();
            ret = cmd.ExecuteNonQuery();
            return ret;
        }
        [HttpPost("UpdateRegion")]
        public int UpdateRegion([FromBody]Region region)
        {
            int ret = -1;
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @"UPDATE REGIONS
                                SET
	                                region_name = '" + region.Name + "' " +
                                "WHERE region_id = '" + region.ID + "'";
            con.Open();
            ret = cmd.ExecuteNonQuery();
            return ret;
        }
        [HttpPost("UpdateDepartment")]
        public int UpdateDepartment([FromBody]Department department)
        {
            int ret = -1;
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @"UPDATE DEPARTMENTS
                                SET
	                                department_name = '" + department.Name + "', " +
                                    "location_id = '" + department.LocationID + "' " +
                                "WHERE department_id = '" + department.ID + "'";
            con.Open();
            ret = cmd.ExecuteNonQuery();
            return ret;
        }
        [HttpPost("UpdateJob")]
        public int UpdateJob([FromBody]Job job)
        {
            int ret = -1;
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = @"UPDATE JOBS
                                SET
	                                job_title = '" + job.Title + "', " +
                                    "min_salary = '" + job.MinSalary + "', " +
                                    "max_salary = '" + job.MaxSalary + "' " +
                                "WHERE job_id = '" + job.ID + "'";
            con.Open();
            ret = cmd.ExecuteNonQuery();
            return ret;
        }

        //--------------------------------------------
        //Deletes
        [HttpDelete("DeleteEmployee")]
        public int DeleteEmployee(int employeeID)
        {
            int ret = -1;
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "DELETE FROM EMPLOYEES where employee_id = '" + employeeID + "'";
            con.Open();
            ret = cmd.ExecuteNonQuery();
            return ret;
        }
        [HttpDelete("DeleteDependent")]
        public int DeleteDependent(int dependenID)
        {
            int ret = -1;
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "DELETE FROM DEPENDENTS where dependent_id = '" + dependenID + "'";
            con.Open();
            ret = cmd.ExecuteNonQuery();
            return ret;
        }
        [HttpDelete("DeleteLocation")]
        public int DeleteLocation(int locationID)
        {
            int ret = -1;
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE DEPARTMENTS SET location_id = '' WHERE location_ID = '" + locationID + "' DELETE FROM LOCATIONS where location_id = '" + locationID + "'";
            con.Open();
            ret = cmd.ExecuteNonQuery();
            return ret;
        }
        [HttpDelete("DeleteCountry")]
        public int DeleteCountry(int countryID)
        {
            int ret = -1;
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "DELETE FROM COUNTRIES where country_id = '" + countryID + "'";
            con.Open();
            ret = cmd.ExecuteNonQuery();
            return ret;
        }
        [HttpDelete("DeleteRegion")]
        public int DeleteRegion(int regionID)
        {
            int ret = -1;
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE COUNTRIES SET region_id = '' WHERE refion_id = '" + regionID + "' DELETE FROM REGIONS where region_id = '" + regionID + "'";
            con.Open();
            ret = cmd.ExecuteNonQuery();
            return ret;
        }
        [HttpDelete("DeleteDepartment")]
        public int DeleteDepartment(int departmentID)
        {
            int ret = -1;
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = " DELETE FROM DEPARTMENTS where department_id = '" + departmentID + "'";
            con.Open();
            ret = cmd.ExecuteNonQuery();
            return ret;
        }
        [HttpDelete("DeleteJob")]
        public int DeleteJob(int jobID)
        {
            int ret = -1;
            SqlConnection con = new SqlConnection("Server=tcp:mcroninpersonal.database.windows.net,1433;Initial Catalog=ESC;Persist Security Info=False;User ID=mcroninSQL;Password=TestingDB1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = " DELETE FROM JOBS where job_id = '" + jobID + "'";
            con.Open();
            ret = cmd.ExecuteNonQuery();
            return ret;
        }

        #endregion
    }
}
