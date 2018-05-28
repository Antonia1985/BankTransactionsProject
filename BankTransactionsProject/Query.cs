using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BankTransactionsProject
{
    public abstract class Query
    {
        public static decimal AddQuerry(string querry)
        {

            string connectionString = @"Server = SKOUNTOUFLAPTOP\SQLEXPRESS; Database = afdemp_csharp_1; Trusted_Connection=True;";
            SqlConnection sqlconn = new SqlConnection(connectionString);
            decimal account = 0;            
            using (sqlconn)
            {
                try
                {
                    sqlconn.Open();
                    SqlCommand cmd = new SqlCommand(querry, sqlconn);                    
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        account = reader.GetDecimal(0);
                    }

                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.Message);
                }
                //finally
                //{
                //    Console.ReadLine();
                //}                
            }
            Login.CloseConnection();
            return account;


        }
        public static decimal ExecuteQuery(string querry)
        {

            string connectionString = @"Server = SKOUNTOUFLAPTOP\SQLEXPRESS; Database = afdemp_csharp_1; Trusted_Connection=True;";
            SqlConnection sqlconn = new SqlConnection(connectionString);
            decimal account = 0;
            using (sqlconn)
            {
                try
                {
                    sqlconn.Open();
                    SqlCommand cmd = new SqlCommand(querry, sqlconn);
                    int result = cmd.ExecuteNonQuery();                    
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.Message);
                }                
            }
            Login.CloseConnection();
            return account;
        }

        public static int AddQuerryInt(string querry)
        {

            string connectionString = @"Server = SKOUNTOUFLAPTOP\SQLEXPRESS; Database = afdemp_csharp_1; Trusted_Connection=True;";
            SqlConnection sqlconn = new SqlConnection(connectionString);
            int account = 0;
            using (sqlconn)
            {
                try
                {
                    sqlconn.Open();
                    SqlCommand cmd = new SqlCommand(querry, sqlconn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        account = reader.GetInt32(0);
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.Message);
                }
                //finally
                //{
                //    Console.ReadLine();
                //}
            }
            Login.CloseConnection();
            return account;
        }
    }
}
