using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NJUArchery_SQL_Component
{
    internal class Tool
    {
        public static string ReadLineWithPrompt(string Prompt, bool silent = false)
        {
            if (!silent)
            {
                Console.Write(Prompt + ":");
                return Console.ReadLine() ?? "";
            }

            else
            {
                Console.Write(Prompt + ":");

                var password = string.Empty;

                ConsoleKeyInfo key;
                while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter)
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        Console.Write("\b \b");
                        password = password.Substring(0, password.Length - 1);
                    }
                    else if (key.Key != ConsoleKey.Backspace)
                    {
                        Console.Write("*");
                        password += key.KeyChar;
                    }
                }

                Console.WriteLine();
                return password;
            }
        }
    }
}
