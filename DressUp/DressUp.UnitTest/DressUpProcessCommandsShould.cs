using NUnit.Framework;
using DressUp.ReferenceTypes;

namespace DressUp.UnitTest
{
    [TestFixture]
    public class DressUpProcessCommandsShould
    {
        [TestCaseSource(typeof(DressUpProcessCommandsTestCases), nameof(DressUpProcessCommandsTestCases.PositiveTestCases))]
        [TestCaseSource(typeof(DressUpProcessCommandsTestCases), nameof(DressUpProcessCommandsTestCases.FailTestCases))]
        public void ProcessFullListofCommands(Temperature temperature, string[] command, string expectedOutput)
        {
            var actual = DressUpManager.ProcessCommands(temperature, command);

            Assert.AreEqual(actual.ToLower(), expectedOutput.ToLower());
        }


    }
}
