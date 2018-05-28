using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BankTransactionsProject
{
    class UsersMenu : Query
    {
        public static  List<BankAccount> listBankAccount = new List<BankAccount>();      

        private static bool CheckUsernameToTransfer(string userName, string userToTransfer)// eos position 6
        {
            string query = "SELECT COUNT (username) FROM users WHERE username = '" + userToTransfer + "'";
            int result = Convert.ToInt32(AddQuerryInt(query));
            bool invalidUsername = false;

            if (result == 0.000 )
            {
                Console.Clear();
                                
                string message ="The user you are looking for does not exists.";               
                ConsoleLayout.Attention(message, 0);
                
                ConsoleLayout.Position(2);
                Console.WriteLine("To choose an other username please press 'Enter'");

                ConsoleLayout.Position(4);
                Console.WriteLine(" To go back to menou please type 'M'");

                string message1 = "Who's that?";
                ConsoleLayout.Speech(message1, -4);

                ConsoleLayout.Position(6);
                string userinput = Convert.ToString(Console.ReadLine());

                if ((userinput== "M")|| (userinput == "m"))
                {
                    ShowMenus.ChooseUser(userName);
                }               
                invalidUsername = true;

            }
            return invalidUsername;
        }

        private static string CheckUserToTransferMethod(string userName, string receiversUsername, string message)
        {
            do
            {
                Console.Clear();

                ConsoleLayout.Position(0);
                Console.WriteLine(message);

                ConsoleLayout.Position(2);
                receiversUsername = Convert.ToString(Console.ReadLine());
            }
            while (CheckUsernameToTransfer(userName, receiversUsername));

            return receiversUsername;
        }

        private static bool CheckAmountInput(string amount, string userName) // eos position 6
        {
            bool isNotDecimal = false;
            decimal num = 0.00m;
            if (!decimal.TryParse(amount, out num))
            {
                Console.Clear();
                string message = "Not valid input!";
                ConsoleLayout.Attention(message, 0);
                    
                ConsoleLayout.Position(2);
                Console.WriteLine("To choose a correct amount please press 'Enter'");
                    
                ConsoleLayout.Position(4);
                Console.WriteLine("To go back to menou please type 'M' and press enter");

                string message1 = "What's this?";
                ConsoleLayout.Speech(message1, -4);

                ConsoleLayout.Position(6);
                string userinput = Convert.ToString(Console.ReadLine());


                if ((userinput == "M") || (userinput == "m"))
                {
                    ShowMenus.ChooseUser(userName);
                }
                                
                isNotDecimal = true;
                Console.Clear();
            }
            return isNotDecimal;
            
        }

        private static string CheckAmountMethod(string userName, string amount, string message)
        {

            do
            {
                Console.Clear();
                ConsoleLayout.Position(0);
                Console.WriteLine(message);
                //System.Threading.Thread.Sleep(2000);
                ConsoleLayout.Position(2);
                amount = Convert.ToString(Console.ReadLine());
            }
            while (CheckAmountInput(amount, userName));

            return amount;
        }
   
        public static decimal ViewMyAccount(string username)
        {            
            string query = "SELECT amount FROM accounts WHERE EXISTS (SELECT username FROM users WHERE (id = accounts.user_id) AND (username = '" + username + "'))";
            decimal account = Convert.ToDecimal(AddQuerry(query));

            ConsoleLayout.Position(0);
            Console.WriteLine("You current balance is: {0}", account);

            ConsoleLayout.Position(2);
            Console.ReadLine();
            Login.CloseConnection();
            return account; 
        }
                
        public static decimal ViewOthersAccount(string userName)
        {
            string usernameToTransfer = "";

            do
            {
                Console.Clear();
                
                ConsoleLayout.Position(0);
                Console.WriteLine("Please choose the username account you wish to view");

                ConsoleLayout.Position(2);
                usernameToTransfer = Console.ReadLine();

            } while (CheckUsernameToTransfer(userName ,usernameToTransfer));
                       
            decimal account = 0;

            string query = "SELECT amount FROM accounts WHERE EXISTS (SELECT username FROM users WHERE (id = accounts.user_id) AND (username = '" + usernameToTransfer + "'))";
            account = Convert.ToDecimal(AddQuerry(query));

            ConsoleLayout.Position(8);
            Console.WriteLine("USER: {0} AMOUNT: {1} ", usernameToTransfer, account);

            ConsoleLayout.Position(10);
            Console.ReadLine();
            Login.CloseConnection();
            
            return account;
        }

        public static void DepositTo(string userName)
        {
            
            string receiversUsername = "";
            string message = "Please enter the username you wish to deposit to: ";
            receiversUsername = CheckUserToTransferMethod(userName, receiversUsername, message);
            
            string amount = "";
            string message2 = "Please enter the amount you wish to transfer: ";
            amount = CheckAmountMethod(userName, amount, message2);
            
            decimal amountToTransfer = Convert.ToDecimal(amount); 

            string query1 = "SELECT amount FROM accounts WHERE EXISTS(SELECT username FROM users WHERE id = accounts.user_id AND username = '" + userName + "')";
            decimal availableDepositersAmount = Convert.ToDecimal(AddQuerry(query1));

            while (availableDepositersAmount < amountToTransfer)
            {
                Console.Clear();
                string message1 = "Sorry, no available amount to transfer, try again!";
                ConsoleLayout.Attention(message1, 0);
                System.Threading.Thread.Sleep(2000);
                amount = CheckAmountMethod(userName, amount, message2);
                amountToTransfer = Convert.ToDecimal(amount);
            }

            string query2 = "SELECT amount FROM accounts WHERE EXISTS(SELECT username FROM users WHERE id = accounts.user_id AND username = '" + receiversUsername + "')";
            decimal receiversAccount = Convert.ToDecimal(AddQuerry(query2));

            decimal depositorsNewbal = availableDepositersAmount - amountToTransfer;
            decimal receiversNewBal = receiversAccount + amountToTransfer;
            decimal newBalance = 0;

            string query3 = "UPDATE accounts SET amount = " + depositorsNewbal + " WHERE EXISTS (SELECT username FROM users WHERE (id = accounts.user_id) AND (username = '" + userName + "')); UPDATE accounts SET amount = " + receiversNewBal + "WHERE exists (SELECT username FROM users WHERE (id = accounts.user_id) and (username = '" + receiversUsername + "'))";
            ExecuteQuery(query3);
            string query4 = "SELECT amount FROM accounts WHERE EXISTS (SELECT username FROM users WHERE (id = accounts.user_id) AND (username = '" + userName + "'))";
            newBalance = Convert.ToDecimal(AddQuerry(query4));
            ConsoleLayout.Position(8);
            Console.WriteLine("Transaction was successfully completed!");
            ConsoleLayout.Position(9);
            Console.WriteLine("New bank balance: {0}", newBalance); 
            System.Threading.Thread.Sleep(3000);

            Login.CloseConnection();

            DateTime time = DateTime.Now;            
            BankAccount Transaction = new BankAccount(time, userName, amountToTransfer, KindOfTransaction.Deposit, receiversUsername);
            BankAccount.AddList(userName, Transaction);
            
        }

        public static void DepositToCooperative(string userName)
        {
            string amount = "";
            string message1 = "Please enter the amount you wish to transfer: ";
            amount = CheckAmountMethod(userName, amount, message1);
                       
            decimal amountToTransfer = Convert.ToDecimal(amount);

            string query1 = "SELECT amount FROM accounts WHERE EXISTS (SELECT username FROM users WHERE (id = accounts.user_id) and (username = '" + userName + "'))";
            decimal availableDepositorsAmount = Convert.ToDecimal(AddQuerry(query1));           
            
            while (availableDepositorsAmount < amountToTransfer)
            {
                Console.Clear();               
                string message ="Sorry, no available amount to transfer, try again!";
                ConsoleLayout.Attention(message, 0);
                System.Threading.Thread.Sleep(2000);
                amount = CheckAmountMethod(userName, amount, message1);
                amountToTransfer = Convert.ToDecimal(amount);
            }

            string query2 = "SELECT amount FROM accounts WHERE EXISTS (SELECT username FROM users WHERE (id = accounts.user_id) and (username = 'admin'))";
            decimal AdminsAmount = Convert.ToDecimal(AddQuerry(query2));

            decimal depositorsNewbal = availableDepositorsAmount - amountToTransfer;
            decimal receiversNewBal = AdminsAmount + amountToTransfer;
            decimal newBalance = 0;

            string query3 = "UPDATE accounts SET amount = " + depositorsNewbal + " WHERE exists (SELECT username FROM users WHERE (id = accounts.user_id) AND (username = '" + userName + "')); UPDATE accounts SET amount = " + receiversNewBal + "WHERE exists (SELECT username FROM users WHERE (id = accounts.user_id) and (username = 'admin'))";
            ExecuteQuery(query3);
            string query4 = "SELECT amount FROM accounts WHERE EXISTS (SELECT username FROM users WHERE (id = accounts.user_id) AND (username = '" + userName + "'))";
            newBalance = Convert.ToDecimal(AddQuerry(query4));

            ConsoleLayout.Position(6);
            Console.WriteLine("Transaction was successfully completed!");
            ConsoleLayout.Position(8);
            Console.WriteLine("New bank balance: {0} euros", newBalance);
            System.Threading.Thread.Sleep(3000);

            Login.CloseConnection();

            DateTime time = DateTime.Now;
            BankAccount Transaction = new BankAccount(time, userName, amountToTransfer, KindOfTransaction.Deposit, "admin");
            BankAccount.AddList(userName, Transaction);
            
        }

        public static void Withdraw(string userName)
        {
            string userToWithdraw = "";
            string message1 = "Please choose the name of the user you wish to withdraw from: ";
            userToWithdraw = CheckUserToTransferMethod(userName, userToWithdraw, message1);
            
            string amount = "";
            string message2 = "Please insert the amount to be withdrawn";
            amount = CheckAmountMethod(userName, amount, message2);
            
            decimal amountToWithdraw = Convert.ToDecimal(amount);
            
            string query1 = "SELECT amount FROM accounts WHERE EXISTS (SELECT username FROM users WHERE (id = accounts.user_id) and (username = '" + userToWithdraw + "'))";
            decimal availableUsersAmount = Convert.ToDecimal(AddQuerry(query1));

            while (availableUsersAmount < amountToWithdraw)
            {
                Console.Clear();
                string message = "Sorry, no available amount to withdraw, try again!";
                ConsoleLayout.Attention(message, 0);
                System.Threading.Thread.Sleep(2000);
                amount = CheckAmountMethod(userName, amount, message2);                
                amountToWithdraw = Convert.ToDecimal(amount);
            }

            string query2 = "SELECT amount FROM accounts WHERE EXISTS (SELECT username FROM users WHERE (id = accounts.user_id) and (username = 'admin'))";
            decimal adminsAmount = Convert.ToDecimal(AddQuerry(query2));

            decimal usersNewbal = availableUsersAmount - amountToWithdraw;
            decimal adminsNewBal = amountToWithdraw + adminsAmount;
            decimal newBalance = 0;

            string query3 = "UPDATE accounts SET amount = " + usersNewbal + " WHERE exists (SELECT username FROM users WHERE (id = accounts.user_id) AND (username = '" + userToWithdraw + "')); UPDATE accounts SET amount = " + adminsNewBal + "WHERE exists (SELECT username FROM users WHERE (id = accounts.user_id) and (username = 'admin'))";
            ExecuteQuery(query3);
            string query4 = "SELECT amount FROM accounts WHERE EXISTS (SELECT username FROM users WHERE (id = accounts.user_id) AND (username = 'admin'))";
            newBalance = Convert.ToDecimal(AddQuerry(query4));

            ConsoleLayout.Position(6);
            Console.WriteLine("Transaction was successfully completed!");
            ConsoleLayout.Position(8);
            Console.WriteLine("New bank balance: {0} euros", newBalance);
            System.Threading.Thread.Sleep(3000);

            Login.CloseConnection();

            DateTime time = DateTime.Now;
            BankAccount Transaction = new BankAccount(time, "admin", amountToWithdraw, KindOfTransaction.Withdrawal, userToWithdraw);
            BankAccount.AddList("admin", Transaction);            
        }

        public static void ExitApplication(string userName)
        {
            ConsoleLayout.Position(0);
            string title = @"
                                      _                _                
                                     | |              | |            |||
                                     | |         _    | |         _  |||
                                     |/ \_|   | |/    |/ \_|   | |/  |||
                                      \_/  \_/|/|__/   \_/  \_/|/|__/ooo
                                             /|               /|        
                                             \|               \|        ";
            Console.WriteLine(title);
            string message = "Bye bye" + userName;
            ConsoleLayout.Speech(message);
            ConsoleLayout.Position(12);
            Environment.Exit(0);
            
        }

        


    }
}
