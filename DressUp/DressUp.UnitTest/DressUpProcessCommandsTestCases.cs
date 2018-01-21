using System.Collections;
using DressUp.ReferenceTypes;
using NUnit.Framework;

namespace DressUp.UnitTest
{
    [TestFixture]
    public class DressUpProcessCommandsTestCases
    {
        public static IEnumerable PositiveTestCases
        {
            get
            {
                yield return
                    new TestCaseData(Temperature.Hot, new[] {"8", "6", "4", "2", "1", "7"},
                        "Removing PJs, shorts, t-shirt, sun visor, sandals, leaving house");

                yield return
                    new TestCaseData(Temperature.Cold, new[] {"8", "6", "3", "4", "2", "5", "1", "7"},
                        "Removing PJs, pants, socks, shirt, hat, jacket, boots, leaving house");

            }
        }

        public static IEnumerable FailTestCases
        {
            get
            {
                yield return
                    new TestCaseData(Temperature.Hot, new[] { "8", "6", "6" },
                        "Removing PJs, shorts, fail");

                yield return
                    new TestCaseData(Temperature.Hot, new[] { "8", "6", "3" },
                        "Removing PJs, shorts, fail");
                
               yield  return new TestCaseData(Temperature.Hot, new[] { "8", "6", "3" },
                    "Removing PJs, shorts, fail");

                yield return
                    new TestCaseData(Temperature.Cold, new[] { "8", "6", "3", "4", "2", "5", "7" },
                        "Removing PJs, pants, socks, shirt, hat, jacket, fail");

                yield return
                    new TestCaseData(Temperature.Cold, new[] { "6" },
                        "fail");

                yield return
                    new TestCaseData(Temperature.Cold, new[] { "87" },
                        "fail");
            }
        }
    }
}
