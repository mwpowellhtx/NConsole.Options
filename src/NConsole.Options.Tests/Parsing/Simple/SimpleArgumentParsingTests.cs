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
            , string[] args, bool[] expectedValues, string[] expectedUnprocessedArgs)
            => base.Can_Parse_Arguments(prototype, description, args, expectedValues, expectedUnprocessedArgs);
#pragma warning restore xUnit1008

#pragma warning disable xUnit1008
        /// <inheritdoc />
        [ClassData(typeof(Data.Parsing.ExpectThrownUnprocessedOptionsTestCases))]
        public override void Did_Throw_On_Unprocessed_Options(string[] prototypes, string[] args
            , string[] unprocessedPrototypes)
            => base.Did_Throw_On_Unprocessed_Options(prototypes, args, unprocessedPrototypes);
#pragma warning restore xUnit1008

    }
}
