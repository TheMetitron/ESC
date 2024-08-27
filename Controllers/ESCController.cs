using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Text.Json;

namespace ESC_Assessment.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ESCController : Controller
    {

        [HttpGet("GetAllData")]
        public string GetFullData()
        {
            //List<Employees> ldm = new List<Employees>();
            SqlConnection con = new SqlConnection("Data Source=USER-PC\\SQLEXPRESS;Integrated Security=True; Database=ESC; TRUSTSERVERCERTIFICATE=true ");
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
            //ldm = JsonSerializer.Deserialize<List<Employees>>(ret) ?? new List<Employees>();
            return ret;
        }
        [HttpGet("GetPartialData")]
        public string GetData(int? employeeID = null, string fName = "", string lName = "", string email = "", string depName = "", string countyName = "", string regName = "")
        {
            //List<Employees> ldm = new List<Employees>();
            SqlConnection con = new SqlConnection("Data Source=USER-PC\\SQLEXPRESS;Integrated Security=True; Database=ESC; TRUSTSERVERCERTIFICATE=true ");
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
            //if (ret.Length > 0)
            //{
            //    ldm = JsonSerializer.Deserialize<List<Employees>>(ret) ?? new List<Employees>();
            //}
            return ret;
        }
    }
}
