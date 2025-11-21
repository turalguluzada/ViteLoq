using System.Text;

namespace Restaurant.Shared.Extensions;

public static class Helpers
{
    public static int GiveNumber(string text)
    {
        Console.Write($"Please enter {text} : ");
        tryAgain:
        int number = 0;
        try
        {
            number = Convert.ToInt32(Console.ReadLine());
        }
        catch (Exception e)
        {
            Console.Write("Wrong type.Please enter a number: ");
            goto tryAgain;
        }
        return number;
    }

    public static string GiveString(string text)
    {
        Console.Write($"Please enter {text} :  ");
        tryAgain:
        string word = Console.ReadLine();
        if (String.IsNullOrEmpty(word))
        {
            Console.WriteLine("Wrong text type.Please enter a text: ");
            goto tryAgain;
        }
        return word;
    }

    public static DateTime GiveDate(string text)
    {
        Console.Write($"Please enter {text} (yyyy-mm-dd) :  ");
        DateTime date = DateTime.Now;
        tryAgain:
        string input = Console.ReadLine();
        try
        {
            date = DateTime.ParseExact(input, "yyyy-MM-dd", null);
        }
        catch (Exception e)
        {
            Console.WriteLine("Wrong date.Please enter a date (yyyy-mm-dd): ");
            goto tryAgain;
        }
        if (date == null || date <= DateTime.Now)
        {
            Console.WriteLine("Wrong date. Please enter a date (yyyy-mm-dd): ");
            goto tryAgain;
        }
        return date;
    }

    public static string GiveConditionalNumber(string text, int number, string condition)
    {
        Console.Write($"Please enter {text} :  ");
        tryAgain:
        string cardNumber = Console.ReadLine();
        if (cardNumber.Length != number)
        {
            Console.Clear();
            Console.WriteLine($"Wrong card number type. Please enter a card number" +
                              $"\n {condition}");
            goto tryAgain;
        }

        for (int i = 0; i < number; i++)
        {
            try
            {
                Convert.ToInt32(cardNumber[i]);
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine($"Wrong card number type. Please enter a card number" +
                                  $"\n {condition}");
                goto tryAgain;
            }
        }
        
        return cardNumber;
    }

    public static string GivePassword()
    {
        Console.Write("Please enter password: ");
        tryAgain:
        string password = Console.ReadLine();
        bool hasUpper = false;
        bool hasLower = false;
        bool hasDigit = false;
        bool hasSpecial = false;

        if (password.Length < 8)
        {
            Console.Write("Password must be at least 8 characters and include ( A-Z, a-z, 0-9, !@#$% )");
            goto tryAgain;
        }

        foreach (char c in password)
        {
            int ascii = (int)c;
            if (ascii >= 65 && ascii <= 90) hasUpper = true;         
            else if (ascii >= 97 && ascii <= 122) hasLower = true;   
            else if (ascii >= 48 && ascii <= 57) hasDigit = true;    
            else hasSpecial = true;                                 
        }
        if (hasUpper == true && hasLower == true &&  hasDigit == true && hasSpecial == true)
            return password;
        
        Console.Write("Password must be at least 8 characters and include ( A-Z, a-z, 0-9, !@#$% )");
        goto tryAgain;
    }

    public static string GiveEmail()
    {
        Console.Write("Please enter email (e.g. example@mail.com): ");
        tryAgain:
        string email = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(email))
        {
            Console.Write("Error: Email cannot be empty. Example: example@mail.com: ");
            goto tryAgain;
        }
        if (email.Contains(" "))
        {
            Console.Write("Error: Email must not contain spaces. Example: example@mail.com: ");
            goto tryAgain;
        }
        if (!email.Contains("@"))
        {
            Console.Write("Error: Email must contain '@'. Example: example@mail.com: ");
            goto tryAgain;
        }
        foreach (char c in email)
        {
            if (char.IsLetter(c) && char.IsUpper(c))
            {
                Console.Write("Error: All letters must be lowercase. Example: example@mail.com: ");
                goto tryAgain;
            }
        }
        return email;
    }

    public static string NormalizedBankCardNumber(string bankCardNumber)
    {
        StringBuilder normalized = new StringBuilder();
        for (int i = 0; i < bankCardNumber.Length; i++)
        {            
            if (i % 4 == 0 && i != 0)
            {
                normalized.Append(" - ");
                
            }
            normalized.Append(bankCardNumber[i]);
        }
        return normalized.ToString();
    }
    public static void Loading(int choose)
    {
        int mSeconds = 1000;
        switch (choose)
        {
            case 0:
                mSeconds = 0; break;
            case 1:
                mSeconds = 200; break;
            case 2:
                mSeconds = 400; break;
            case 3:
                mSeconds = 600; break;
            case 4:
                mSeconds = 800; break;
            case 5:
                mSeconds = 1000; break;
            case 6:
                mSeconds = 1200; break;
            case 7:
                mSeconds = 1400; break;
            case 8:
                mSeconds = 1600; break;
            case 9:
                mSeconds = 1800; break;
            case 10:
                mSeconds = 2000; break;
        }
        // mSeconds = 0;
        int totalBlocks = 30; 
        int delay = mSeconds / totalBlocks;

        Console.CursorVisible = false;
        Console.Clear();

        int centerY = Console.WindowHeight / 2;
        int centerX = (Console.WindowWidth / 2) - (totalBlocks / 2);

        Console.SetCursorPosition(centerX, centerY);
        Console.Write("["); 

        Console.SetCursorPosition(centerX + totalBlocks + 1, centerY);
        Console.Write("]"); 

        for (int i = 0; i <= totalBlocks; i++)
        {
            Console.SetCursorPosition(centerX + 1 + i, centerY);
            Console.Write("#"); 

            Thread.Sleep(delay); 
        }

        Console.SetCursorPosition(centerX, centerY + 2);
        Console.CursorVisible = true;
        Console.Clear();
    }
}