namespace NConsole.Options.Parsing.Simple
{
    using Xunit;
    using Xunit.Abstractions;

    public class SimpleArgumentParsingTests : SimpleArgumentParsingTestFixtureBase
    {
        public SimpleArgumentParsingTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

#pragma warning disable xUnit1008
        /// <inheritdoc />
        [ClassData(typeof(Data.Parsing.SimpleOptionSetParsingTestCases))]
        public override void Can_Parse_Arguments(string prototype, string description
            , string[] args, bool[] expectedValues, string[] expectedUnprocessedArgs
        )
        {
            Callback = () => OptionsVisited.Add(true);

            base.Can_Parse_Arguments(prototype, description, args, expectedValues, expectedUnprocessedArgs);
        }
#pragma warning restore xUnit1008

    }
}
