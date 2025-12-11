using System;
using System.IO;
using System.Security.Principal;
using ConsoleApp3;

Console.WriteLine("Welcome to the ATM.");
Console.WriteLine();

const int realPin = 1234;
int tries = 0;
bool pinOk = false;

while (tries < 3 && !pinOk)
{
    Console.Write("Enter PIN: ");
    string pinText = Console.ReadLine();
    int pin;

    if (int.TryParse(pinText, out pin))
    {
        if (pin == realPin)
        {
            pinOk = true;
        }
        else
        {
            Console.WriteLine("Wrong PIN.");
            tries++;
        }
    }
    else
    {
        Console.WriteLine("Please enter numbers only.");
    }
}

if (!pinOk)
{
    Console.WriteLine("Too many tries. Card is blocked.");
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
    return;
}

decimal startBalance = LoadBalance();
Account acc = new Account(startBalance);

int choice = -1;

while (choice != 7)
{
    Console.WriteLine();
    Console.WriteLine("Menu:");
    Console.WriteLine("1 - Show balance");
    Console.WriteLine("2 - Withdraw");
    Console.WriteLine("3 - Deposit");
    Console.WriteLine("4 - Show full history");
    Console.WriteLine("5 - Filter history (LINQ)");
    Console.WriteLine("6 - Delete history item (CRUD)");
    Console.WriteLine("7 - Exit");
    Console.Write("Choose option: ");

    string choiceText = Console.ReadLine();

    if (!int.TryParse(choiceText, out choice))
    {
        Console.WriteLine("You must enter a number.");
        continue;
    }

    Console.WriteLine();

    switch (choice)
    {
        case 1:
            acc.ShowBalance();
            break;

        case 2:
            Console.Write("Enter amount to withdraw: ");
            string wText = Console.ReadLine();
            if (decimal.TryParse(wText, out decimal wAmount))
            {
                acc.Withdraw(wAmount);
                SaveBalance(acc.GetBalance());
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
            break;

        case 3:
            Console.Write("Enter amount to deposit: ");
            string dText = Console.ReadLine();
            if (decimal.TryParse(dText, out decimal dAmount))
            {
                acc.Deposit(dAmount);
                SaveBalance(acc.GetBalance());
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
            break;

        case 4:
            acc.ShowHistory();
            break;

        case 5:
            Console.Write("Enter filter word (DEPOSIT / WITHDRAW): ");
            string filter = Console.ReadLine();
            acc.FilterHistory(filter);
            break;

        case 6:
            acc.DeleteHistoryItem();
            break;

        case 7:
            Console.WriteLine("Thank you for using our ATM.");
            break;

        default:
            Console.WriteLine("No such menu option.");
            break;
    }
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();


static decimal LoadBalance()
{
    try
    {
        string path = "account.xml";

        if (!File.Exists(path))
            return 1000m;

        string xml = File.ReadAllText(path);

        int start = xml.IndexOf("<Balance>") + "<Balance>".Length;
        int end = xml.IndexOf("</Balance>");

        string number = xml.Substring(start, end - start);

        if (decimal.TryParse(number, out decimal result))
            return result;
    }
    catch
    {

    }

    return 1000m;
}


static void SaveBalance(decimal balance)
{
    try
    {
        string xmlText =
            "<Account>\n" +
            "  <Balance>" + balance + "</Balance>\n" +
            "</Account>";

        string jsonText =
            "{\n" +
            "  \"Balance\": " + balance.ToString().Replace(',', '.') + "\n" +
            "}";

        File.WriteAllText("account.xml", xmlText);
        File.WriteAllText("account.json", jsonText);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Could not save files: " + ex.Message);
    }
}
