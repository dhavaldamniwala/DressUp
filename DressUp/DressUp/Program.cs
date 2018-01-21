using DressUp.ReferenceTypes;
using System;

namespace DressUp
{
    internal class Program
    {
        private static Temperature _temperature;
        private static string[] _commands;

        private static void Main()
        {
            GetAndProcessInput();
        }

        private static void GetAndProcessInput()
        {
            while (true)
            {
                Console.WriteLine("Enter the Temperature Type (Hot or Cold) followed by comma seperated list of commands");
                var consoleInput = Console.ReadLine();

                if (consoleInput != null)
                {
                    if (!ValidateInput(consoleInput))
                    {
                        Console.WriteLine("\nWrong Input, Enter Again");
                        continue;
                    }

                    _commands =
                        consoleInput.Substring(_temperature.ToString().Length,
                            consoleInput.Length - _temperature.ToString().Length).Replace(" ", string.Empty).Split(',');

                    Console.WriteLine(DressUpManager.ProcessCommands(_temperature, _commands));
                }

                Console.WriteLine("\nDo you want to continue Y/N");
                var shouldContinue = Console.ReadLine();
                if (shouldContinue != null && shouldContinue.Equals("n", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
            }
        }

        private static bool ValidateInput(string input)
        {
            var inputSplit = input.Split(' ');
            return inputSplit.Length >= 2
                   && !string.IsNullOrEmpty(inputSplit[1])
                   && Enum.TryParse(inputSplit[0], true, out _temperature);
        }
    }
}
