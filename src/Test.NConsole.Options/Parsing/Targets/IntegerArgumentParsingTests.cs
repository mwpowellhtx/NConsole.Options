namespace NConsole.Options.Parsing.Targets
{
    using Xunit;
    using Xunit.Abstractions;

    public class IntegerArgumentParsingTests : TargetArgumentParsingTestFixtureBase<int>
    {
        public IntegerArgumentParsingTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

#pragma warning disable xUnit1008
        /// <summary>
        /// Verifies that the <see cref="OptionSet"/> Can Parse the <paramref name="args"/>.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <param name="requiredOrOptional"></param>
        /// <param name="args"></param>
        /// <param name="expectedValues"></param>
        /// <param name="unprocessedArgs"></param>
        /// <inheritdoc />
        [ClassData(typeof(Data.Parsing.Targets.IntegerOptionSetParsingTestCases))]
        public override void Can_Parse_Arguments(string prototype, string description, char requiredOrOptional
            , string[] args, int[] expectedValues, string[] unprocessedArgs)
        {
            Callback = ParsedValues.Add;

            base.Can_Parse_Arguments(prototype, description, requiredOrOptional
                , args, expectedValues, unprocessedArgs);
        }
#pragma warning restore xUnit1008

    }
}
