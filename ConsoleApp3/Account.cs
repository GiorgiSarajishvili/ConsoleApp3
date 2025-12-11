using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Account
    {
        private decimal balance;
        private List<string> history = new List<string>();

        public Account(decimal startBalance)
        {
            if (startBalance < 0)
            {
                balance = 0;
            }
            else
            {
                balance = startBalance;
            }

            history.Add("START BALANCE = " + balance);
        }

        public decimal GetBalance()
        {
            return balance;
        }

        public void ShowBalance()
        {
            Console.WriteLine("Current balance: " + balance + " USD");
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Amount must be greater than zero.");
                return;
            }

            balance += amount;
            history.Add("DEPOSIT: " + amount);

            Console.WriteLine("Deposit successful.");
            ShowBalance();
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Amount must be greater than zero.");
                return;
            }

            if (amount > balance)
            {
                Console.WriteLine("Not enough funds.");
                return;
            }

            balance -= amount;
            history.Add("WITHDRAW: " + amount);

            Console.WriteLine("Please take your cash.");
            ShowBalance();
        }

        public void ShowHistory()
        {
            if (history.Count == 0)
            {
                Console.WriteLine("No history yet.");
                return;
            }

            Console.WriteLine("History:");
            for (int i = 0; i < history.Count; i++)
            {
                Console.WriteLine(i + ": " + history[i]);
            }
        }

        public void FilterHistory(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                Console.WriteLine("Empty filter.");
                return;
            }

            string upper = word.ToUpper();

            var filtered = history
                .Where(h => h.ToUpper().Contains(upper));

            Console.WriteLine("Filtered history:");

            bool any = false;
            foreach (var h in filtered)
            {
                Console.WriteLine(h);
                any = true;
            }

            if (!any)
            {
                Console.WriteLine("No matching entries.");
            }
        }

        public void DeleteHistoryItem()
        {
            if (history.Count == 0)
            {
                Console.WriteLine("No history to delete.");
                return;
            }

            ShowHistory();
            Console.Write("Enter index to delete: ");

            if (int.TryParse(Console.ReadLine(), out int index))
            {
                if (index >= 0 && index < history.Count)
                {
                    history.RemoveAt(index);
                    Console.WriteLine("Item deleted.");
                }
                else
                {
                    Console.WriteLine("Index out of range.");
                }
            }
            else
            {
                Console.WriteLine("Invalid index.");
            }
        }
    }
}



