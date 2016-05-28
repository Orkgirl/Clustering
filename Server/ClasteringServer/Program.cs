using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasteringServer
{
    class Program
    {
        static void Main(string[] args)
        {

            string connectionString =
             "Server=DESKTOP-C2C54ET;" +
             "Database=Clastering;" +
             "User ID=blackhole;" +
             "Password=q1w2e3r4;" +
             "Integrated Security=True";

            SqlConnection sqlConnection1 = new SqlConnection(connectionString);           
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM Region";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            reader = cmd.ExecuteReader();

            Console.WriteLine(reader.GetName(0));
            // Data is accessible through the DataReader object here.

            sqlConnection1.Close();

            Console.ReadKey();
        }
    }
}
