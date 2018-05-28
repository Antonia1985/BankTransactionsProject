using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Security.Cryptography;
using System.Data.Common;
using System.Speech.Synthesis;


namespace BankTransactionsProject
{
    class Login
    {
        public static void Welcome()
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta; 
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            Login.EncryptPasswordOnSQL();
            string title = @"      



                                X
                                X X
                                X X X
                         XXXXXXXX X X
                           XXXXXXXX X         C#X                C#X
                             XXXXXXXX       XXXXXXX            XXXXXXXX
                                     X    XXX      XXX       XXX      XXX
                                       X XX          XXX   XXX          XX
                                        XX              XXX              XX
                                        XX               X               XX
                                        XX     WELCOME                   XX
                                         XX          TO                 XX
                                          XXX         BEAUTY-LINE     XXX
                                           XXXX                     XXXX
                                              XC#X                XC#X
                                                 XXX             XXXX
                                                    XXX        XXX    X
                                                       XX     XX        X
                                                         XXXXX            X X
                                                          C#               C#X
                                                                          XXC#X 
                                MORE HEALTH                                        );(
                                      MORE BEAUTY                .-.     .-.     .           
                                            MORE WELLNESS ... _.'   `._.'   `._.'       
                                ";
            


            Console.WriteLine(title);
            string message = "Hello gorgeous girl!!!\nWelcome to beauty line!";
            ConsoleLayout.Speech(message);            
            Console.Clear();
        }

                
        static public void Connect()
        {
            
            string connectionString = @"Server = SKOUNTOUFLAPTOP\SQLEXPRESS; Database = afdemp_csharp_1; Trusted_Connection=True;";
            SqlConnection sqlconn = new SqlConnection(connectionString);
            using (sqlconn)
            {                
                try
                {
                    sqlconn.Open();                   
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.Message);
                }
                finally
                {
                    CheckUser(sqlconn);                    
                }                
            }

        }

        static public void CloseConnection()
        {
            //SqlConnection sqlconn = SqlConn();
            string connectionString = @"Server = SKOUNTOUFLAPTOP\SQLEXPRESS; Database = afdemp_csharp_1; Trusted_Connection=True;";
            SqlConnection sqlconn = new SqlConnection(connectionString);
            sqlconn.Close();
        }

        public static void EncryptPasswordOnSQL()
        {            
            string query = "ALTER TABLE users ALTER COLUMN password nvarchar(MAX)";
            Query.ExecuteQuery(query);

            string encryptedPassword1 = GenerateSHA256String("password1");
            string query1 = "UPDATE users SET password = '" + encryptedPassword1 + "' WHERE password = 'password1'";
            Query.ExecuteQuery(query1);

            string encryptedPassword2 = GenerateSHA256String("password2");
            string query2 = "UPDATE users SET password = '" + encryptedPassword2 + "' WHERE password = 'password2'";
            Query.ExecuteQuery(query2);

            string encryptedPassword3 = GenerateSHA256String("admin");
            string query3  = "UPDATE users SET password = '" + encryptedPassword3 + "' WHERE password = 'admin'";
            Query.ExecuteQuery(query3);
        }

        public static void CheckUser(SqlConnection sqlconn)
        {
            Welcome();
            int result = 0;
            int i = 2;
            do
            {

                // Console.WriteLine("{0," + Console.WindowWidth / 2 + "}", " ");
                ConsoleLayout.Position(0);
                Console.WriteLine("Please enter your username");
   
                ConsoleLayout.Position(2);
                string userName = Convert.ToString(Console.ReadLine());

                ConsoleLayout.Position(4);
                Console.WriteLine("Please enter your password");

                ConsoleLayout.Position(6);
                string passWord = GenerateSHA256String(Hide());
                

                //sc.Parameters.AddWithValue("@password", GetHashedText(pass.Text));

                SqlCommand cmd = new SqlCommand("SELECT COUNT (*) FROM users WHERE username = @username AND password = @password", sqlconn);
                cmd.Parameters.AddWithValue("@username", userName);
                cmd.Parameters.AddWithValue("@password", passWord);
                result = (int)cmd.ExecuteScalar();

                if (result > 0)
                {
                    ConsoleLayout.Position(10);
                    Console.WriteLine("Successful Connection");

                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                    ShowMenus.ChooseUser(userName);
                    break;
                }
                else if (i == 0)
                {
                    string message1 = "Where is your mind?";
                    ConsoleLayout.Speech(message1, 0);
                    string message = "Sorry, invalid user!!!";
                    ConsoleLayout.Attention(message, 10);                    

                    ConsoleLayout.Position(12);                    
                    Environment.Exit(0);
                    
                }
                else 
                {
                    i--;
                    string message = "Unsuccessfull Connection";
                    ConsoleLayout.Attention(message, 10);
                    ConsoleLayout.Position(12);
                    Console.WriteLine("Please try again, {0} tries left!!!", i+1);

                    System.Threading.Thread.Sleep(1000);
                    Console.Clear();
                }
                
            }
            while ((result == 0) && (i >= 0));           
            
        }
        
        static public string Hide()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);

            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // remove one character from the list of password characters
                        password = password.Substring(0, password.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);


            }
            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();
            return password;
        }

        public static string GenerateSHA256String(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        
        
    }
}
