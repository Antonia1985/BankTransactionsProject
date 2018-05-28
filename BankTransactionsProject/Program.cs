using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using System.Security.Cryptography;

namespace BankTransactionsProject
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Login.Connect();           
        }
    }
}
