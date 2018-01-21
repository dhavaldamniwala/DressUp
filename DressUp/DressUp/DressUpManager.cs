using System;
using System.Collections.Generic;
using System.Linq;
using DressUp.Interface;
using DressUp.ReferenceTypes;

namespace DressUp
{
    public static class DressUpManager
    {
        public static string Output;

        public static string ProcessCommands(Temperature temperature, string[] commands)
        {
            IClothes clothes;
            Output = string.Empty;
            var commandsProcessed = new HashSet<string>();
            
            if (Temperature.Hot.ToString().Equals(temperature.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                clothes = new HotTemperatureClothes();
            }
            else
            {
                clothes = new ColdTemperatureClothes();
            }

            foreach (var command in commands)
            {
                var commandDescription = (ClothesDescription)int.Parse(command);

                if (commandsProcessed.Contains(commandDescription.ToString()))
                {
                    return AppendFailAndReturnOutput();
                }

                if (!ArePajamasRemovedFirst(commandsProcessed, commandDescription))
                {
                    return AppendFailAndReturnOutput();
                }

                switch (commandDescription)
                {
                    case ClothesDescription.PutOnFootwear:
                        if (!AreSocksPutOn(commandsProcessed) && clothes.GetType() != typeof(HotTemperatureClothes))
                        {
                            return AppendFailAndReturnOutput();
                        }

                        if (!ArePantsPutOn(commandsProcessed))
                        {
                            return AppendFailAndReturnOutput();
                        }

                        AppendString(clothes.PutOnFootwear);
                        commandsProcessed.Add(commandDescription.ToString());
                        break;

                    case ClothesDescription.PutOnHeadwear:
                        if (!IsShirtPutOn(commandsProcessed))
                        {
                            return AppendFailAndReturnOutput();
                        }

                        AppendString(clothes.PutOnHeadwear);
                        commandsProcessed.Add(commandDescription.ToString());
                        break;

                    case ClothesDescription.PutOnSocks:
                        if (clothes.GetType() == typeof(HotTemperatureClothes))
                        {
                            return AppendFailAndReturnOutput();
                        }

                        AppendString(clothes.PutOnSocks);
                        commandsProcessed.Add(commandDescription.ToString());
                        break;

                    case ClothesDescription.PutOnShirt:
                        AppendString(clothes.PutOnShirt);
                        commandsProcessed.Add(commandDescription.ToString());
                        break;

                    case ClothesDescription.PutOnJacket:
                        if (clothes.GetType() == typeof(HotTemperatureClothes))
                        {
                            return AppendFailAndReturnOutput();
                        }

                        if (!IsShirtPutOn(commandsProcessed))
                        {
                            return AppendFailAndReturnOutput();
                        }

                        AppendString(clothes.PutOnJacket);
                        commandsProcessed.Add(commandDescription.ToString());
                        break;

                    case ClothesDescription.PutOnPants:
                        AppendString(clothes.PutOnPants);
                        commandsProcessed.Add(commandDescription.ToString());
                        break;

                    case ClothesDescription.LeaveHouse:
                        if (!AreAllTheItemsOfClothingOn(clothes, commandsProcessed))
                        {
                            return AppendFailAndReturnOutput();
                        }

                        AppendString(clothes.LeaveHouse);
                        commandsProcessed.Add(commandDescription.ToString());
                        break;

                    case ClothesDescription.TakeOffPajamas:
                        AppendString(clothes.TakeOffPajamas);
                        commandsProcessed.Add(commandDescription.ToString());
                        break;

                    default:
                        return AppendFailAndReturnOutput();
                }
            }

            return Output;
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
            return Output;
        }

        private static void AppendString(string value)
        {
            if (string.IsNullOrEmpty(Output))
            {
                Output += value;
            }
            else
            {
                Output = Output + ", " + value;
            }
        }
    }
}
