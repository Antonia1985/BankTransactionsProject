using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace BankTransactionsProject
{
    public enum KindOfTransaction
    {
        Deposit, Withdrawal
    }

    class BankAccount : Query
    {

        private string user;
        private string transactionUser;        
        private decimal amount;        
        public KindOfTransaction KindOfTransact { get; set; }
        
        private DateTime transactionDate;    
        private DateTime TransactionDate
        {
            get
            {
                return transactionDate;
            }
            set
            {                
                transactionDate = DateTime.Now;
            }
        }
        
        public BankAccount(DateTime transactionDate, string user, decimal amount, KindOfTransaction kindOfTransact, string transactionUser)
        {
            this.user = user;
            this.amount = amount;
            TransactionDate = transactionDate;
            KindOfTransact = kindOfTransact;
            this.transactionUser = transactionUser;
        }

        public BankAccount()
        {
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Type: ").AppendLine(this.GetType().Name)
                .Append("Kind of transaction: ").AppendLine($"{KindOfTransact}")
                .Append("Username: ").AppendLine(user)                
                .Append("Date of transaction: ").AppendLine($"{TransactionDate}")
                .Append("Amount: ").AppendLine($"{ Currency( amount)}"/* + "c"*/)
                .Append("Transacted to / from: ").Append(transactionUser)
                .AppendLine();

            return sb.ToString();
        }
            
        public static void AddList (string user, BankAccount transaction)
        {
            user = transaction.user;
            UsersMenu.listBankAccount.Add(transaction);
        }

        public static string FileName(string user)
        {
            DateTime datetime = DateTime.Now;
            string date = datetime.ToString("dd_MM_yyyy");
            string fileName = "statement_user_" + user + "_" + date;
            return fileName;
        }
        
        public static void SendToFile(string user)
        {
            List<BankAccount> listBankAccount = new List<BankAccount>();
            BankAccount transaction = new BankAccount();
            listBankAccount = UsersMenu.listBankAccount;
            
            StreamWriter tw = new StreamWriter(@"C:\Users\Antonia\Desktop\BankTransactionsProject\Transaction_Statements\" + FileName(user) + ".txt");

            using (tw)
            {
                foreach (BankAccount s in listBankAccount)
                {
                    tw.WriteLine(s);
                }
            }                

            Console.Clear();
            ConsoleLayout.Position(0);
            Console.WriteLine("Your statement has been sent to your Bank Account Statements!");
            Console.ReadLine();
        }

        public static string Currency(decimal amount)
        {
            CultureInfo ci = new CultureInfo("el-GR");
            ci.NumberFormat.CurrencySymbol = "€";
            CultureInfo.DefaultThreadCurrentCulture = ci;
            string w = amount.ToString("c");
            return w;
        }
    }
}
