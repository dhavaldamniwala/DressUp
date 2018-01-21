using DressUp.ReferenceTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using DressUp.Interface;

namespace DressUp
{
    internal class Program
    {
        private static Temperature _temperature;
        private static string _output;

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
                    var input = consoleInput.Split(' ');
                    if (!ValidateInput(input))
                    {
                        Console.WriteLine("\nWrong Input, Enter Again");
                        continue;
                    }
                    
                    Console.WriteLine(ProcessCommands(input));
                }

                Console.WriteLine("\nDo you want to continue Y/N");
                var shouldContinue = Console.ReadLine();
                if (shouldContinue != null && shouldContinue.Equals("n", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
            }
        }

        private static bool ValidateInput(IReadOnlyList<string> input)
        {
            return input.Count >= 2
                   && Enum.TryParse(input[0], true, out _temperature);
        }

        private static string ProcessCommands(IReadOnlyList<string> commands)
        {
            IClothes clothes;
            var set = new HashSet<string>();
            _output = string.Empty;

            if (Temperature.Hot.ToString().Equals(commands[0], StringComparison.OrdinalIgnoreCase))
            {
                clothes = new HotTemperatureClothes();
            }
            else
            {
                clothes = new ColdTemperatureClothes();
            }

            foreach (var command in commands[1].Split(','))
            {
                var commandDescription = (ClothesDescription) int.Parse(command);

                if (set.Contains(commandDescription.ToString()))
                {
                    return AppendFailAndReturnOutput();
                }

                if (!ArePajamasRemovedFirst(set, commandDescription))
                {
                    return AppendFailAndReturnOutput();
                }

                switch (commandDescription)
                {
                    case ClothesDescription.PutOnFootwear:
                        if (!AreSocksPutOn(set) && clothes.GetType() != typeof(HotTemperatureClothes))
                        {
                            return AppendFailAndReturnOutput();
                        }

                        if (!ArePantsPutOn(set))
                        {
                            return AppendFailAndReturnOutput();
                        }
                        
                        AppendString(clothes.PutOnFootwear);
                        set.Add(commandDescription.ToString());
                        break;

                    case ClothesDescription.PutOnHeadwear:
                        if (!IsShirtPutOn(set))
                        {
                            return AppendFailAndReturnOutput();
                        }
                        AppendString(clothes.PutOnHeadwear);
                        set.Add(commandDescription.ToString());
                        break;

                    case ClothesDescription.PutOnSocks:
                        if (clothes.GetType() == typeof(HotTemperatureClothes))
                        {
                            return AppendFailAndReturnOutput();
                        }

                        AppendString(clothes.PutOnSocks);
                        set.Add(commandDescription.ToString());
                        break;

                    case ClothesDescription.PutOnShirt:
                        AppendString(clothes.PutOnShirt);
                        set.Add(commandDescription.ToString());
                        break;

                    case ClothesDescription.PutOnJacket:
                        if (clothes.GetType() == typeof(HotTemperatureClothes))
                        {
                            return AppendFailAndReturnOutput();
                        }

                        if (!IsShirtPutOn(set))
                        {
                            return AppendFailAndReturnOutput();
                        }

                        AppendString(clothes.PutOnJacket);
                        set.Add(commandDescription.ToString());
                        break;

                    case ClothesDescription.PutOnPants:
                        AppendString(clothes.PutOnPants);
                        set.Add(commandDescription.ToString());
                        break;

                    case ClothesDescription.LeaveHouse:
                        if (!AreAllTheItemsOfClothingOn(clothes, set))
                        {
                            return AppendFailAndReturnOutput();
                        }
                        AppendString(clothes.LeaveHouse);
                        set.Add(commandDescription.ToString());
                        break;

                    case ClothesDescription.TakeOffPajamas:
                        AppendString(clothes.TakeOffPajamas);
                        set.Add(commandDescription.ToString());
                        break;

                    default:
                        return AppendFailAndReturnOutput();
                }
            }

            return _output;
        }

        private static bool AreAllTheItemsOfClothingOn(IClothes clothes, HashSet<string> set)
        {
            if (clothes.GetType() == typeof(HotTemperatureClothes))
            {
                return
                    Enum.GetValues(typeof(ClothesDescription))
                        .Cast<object>()
                        .Where(
                            cd =>
                                cd.ToString() != ClothesDescription.PutOnSocks.ToString() &&
                                cd.ToString() != ClothesDescription.PutOnJacket.ToString() &&
                                cd.ToString() != ClothesDescription.LeaveHouse.ToString())
                        .All(cd => set.Contains(cd.ToString()));
            }
            return
                Enum.GetValues(typeof(ClothesDescription))
                    .Cast<object>()
                    .Where(cd => cd.ToString() != ClothesDescription.LeaveHouse.ToString())
                    .All(cd => set.Contains(cd.ToString()));
        }


        private static bool IsShirtPutOn(ICollection<string> set)
        {
            return set.Contains(ClothesDescription.PutOnShirt.ToString());
        }

        private static bool ArePantsPutOn(ICollection<string> set)
        {
            return set.Contains(ClothesDescription.PutOnPants.ToString());
        }

        private static bool AreSocksPutOn(ICollection<string> set)
        {
            return set.Contains(ClothesDescription.PutOnSocks.ToString());
        }

        private static bool ArePajamasRemovedFirst(IReadOnlyCollection<string> set, ClothesDescription commandDescription)
        {
            if (set.Count == 0 && commandDescription == ClothesDescription.TakeOffPajamas)
            {
                return true;
            }
            return set.Count != 0 && commandDescription != ClothesDescription.TakeOffPajamas;
        }

        private static string AppendFailAndReturnOutput()
        {
            AppendString("fail");
            return _output;
        }

        private static void AppendString( string value)
        {
            if (string.IsNullOrEmpty(_output))
            {
                _output += value;
            }
            else
            {
                _output = _output + ", " + value;
            }
        }
    }
}
