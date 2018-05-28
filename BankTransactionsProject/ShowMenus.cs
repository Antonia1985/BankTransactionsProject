using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BankTransactionsProject
{
    class ShowMenus
    {
       
        public static void ChooseUser(string userName)
        {
                        
            if ((userName == "admin")|| (userName == "ADMIN"))
            {               
                string[] menuItems = { "View my account", "View Members’ bank accounts", "Deposit to Member's bank account", "Withdaw from Member's bank account", "Send to file today's statement", "Exit the application" };

                short curItem = 0, c;
                ConsoleKeyInfo key;
                do
                {
                    Console.Clear();

                    ConsoleLayout.Position(0);
                    Console.WriteLine("~*~*~*~  Hello {0}!  ~*~*~*~ \t", userName);

                    int leftOffSet = (Console.WindowWidth / 3) - 20;
                    int topOffSet = (Console.WindowHeight / 3) + 1;
                    Console.SetCursorPosition(leftOffSet, topOffSet);
                    Console.WriteLine("Please Choose an action to perform and press enter \t\t\t \n");

                    for (c = 0; c < menuItems.Length; c++)
                    {
                        if (curItem == c)
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.DarkGray;

                            //Console.Write(String.Format("{0," + Console.WindowWidth / 2 + "}", ">> "));
                            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", menuItems[c]));
                            //Console.Write(">> ");
                            //Console.WriteLine(menuItems[c]);

                            Console.BackgroundColor = ConsoleColor.DarkMagenta;
                            Console.ForegroundColor = ConsoleColor.White;

                        }
                        else
                        {
                            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", menuItems[c]));
                            //Console.WriteLine(menuItems[c]);
                            
                        }
                    }
                    Console.WriteLine( "\b");
                    
                    key = Console.ReadKey(true);
                    
                    if (key.Key.ToString() == "DownArrow")
                    {
                        curItem++;
                        if (curItem > menuItems.Length - 1) curItem = 0;

                    }
                    else if (key.Key.ToString() == "UpArrow")
                    {
                        curItem--;
                        if (curItem < 0) curItem = Convert.ToInt16(menuItems.Length - 1);
                    }
                } while (key.KeyChar != 13);

                switch (curItem)
                {
                    case 0:
                        Console.Clear();
                        UsersMenu.ViewMyAccount(userName);
                        ChooseUser(userName);
                        break;
                    case 1:
                        Console.Clear();
                        UsersMenu.ViewOthersAccount(userName);
                        ChooseUser(userName);
                        break;
                    case 2:
                        Console.Clear();
                        UsersMenu.DepositTo(userName);                        
                        ChooseUser(userName);
                        break;
                    case 3:
                        Console.Clear();
                        UsersMenu.Withdraw(userName);
                        ChooseUser(userName);
                        break;
                    case 4:
                        BankAccount.FileName(userName);
                        BankAccount.SendToFile(userName);
                        ChooseUser(userName);
                        Console.Clear();
                        break;
                    case 5:
                        Console.Clear();
                        UsersMenu.ExitApplication(userName);
                        //System.Threading.Thread.Sleep(2000);
                        //Environment.Exit(0);
                        break;
                }
            }
            else
            {
                string[] menuItems = { "View my account", "Deposit to Cooperative’s internal bank account", "Deposit to Member's bank account", "Send to file today's statement", "Exit the application" };

                short curItem = 0, c;
                ConsoleKeyInfo key;
                do
                {
                    Console.Clear();

                    ////Console.WriteLine(String.Format("{0," + Console.WindowWidth / 3 + "}", "\tHello {0}!"), userName);
                    ConsoleLayout.Position(0);
                    Console.WriteLine("~*~*~*~  Hello {0}!  ~*~*~*~ \t", userName);

                    ////Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Please Choose an action to perform and press enter"));
                    int leftOffSet = (Console.WindowWidth / 3) - 20;
                    int topOffSet = (Console.WindowHeight / 3) + 2;
                    Console.SetCursorPosition(leftOffSet, topOffSet);
                    Console.WriteLine("Please Choose an action to perform and press enter \t\t\t \n");

                    for (c = 0; c < menuItems.Length; c++)
                    {
                        if (curItem == c)
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.DarkGray;

                            //Console.Write(String.Format("{0," + Console.WindowWidth / 2 + "}", ">> "));
                            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", menuItems[c]));
                            //Console.Write(">> ");
                            //Console.WriteLine(menuItems[c]);

                            Console.BackgroundColor = ConsoleColor.DarkMagenta;
                            Console.ForegroundColor = ConsoleColor.White;

                        }
                        else
                        {
                            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", menuItems[c]));
                            //Console.WriteLine(menuItems[c]);

                        }
                    }
                    Console.WriteLine("\b");

                    key = Console.ReadKey(true);

                    if (key.Key.ToString() == "DownArrow")
                    {
                        curItem++;
                        if (curItem > menuItems.Length - 1) curItem = 0;

                    }
                    else if (key.Key.ToString() == "UpArrow")
                    {
                        curItem--;
                        if (curItem < 0) curItem = Convert.ToInt16(menuItems.Length - 1);
                    }
                } while (key.KeyChar != 13);

                switch (curItem)
                {
                    case 0:
                        Console.Clear();
                        UsersMenu.ViewMyAccount(userName);
                        ChooseUser(userName);
                        break;
                    case 1:
                        Console.Clear();
                        UsersMenu.DepositToCooperative(userName);
                        ChooseUser(userName);
                        break;
                    case 2:
                        Console.Clear();
                        UsersMenu.DepositTo(userName);
                        ChooseUser(userName);
                        break;
                    case 3:
                        Console.Clear();
                        BankAccount.FileName(userName);
                        BankAccount.SendToFile(userName);
                        ChooseUser(userName);
                        break;
                    case 4:
                        Console.Clear();
                        UsersMenu.ExitApplication(userName);
                        break;
                }
            }
        }
    }
}
