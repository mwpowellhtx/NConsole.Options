using System.Collections.Generic;

namespace NConsole.Options.Parsing.KeyValue
{
    using Data.Parsing.KeyValue;
    using Xunit;
    using Xunit.Abstractions;

    public class LongBooleanArgumentParsingTests : KeyValueArgumentParsingTestFixtureBase<long, bool>
    {
        public LongBooleanArgumentParsingTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

#pragma warning disable xUnit1008
        [ClassData(typeof(LongBooleanOptionSetParsingTestCases))]
        public override void Can_Parse_Arguments(string prototype, string description, char requiredOrOptional
            , string[] args, KeyValuePair<long, bool>[] expectedValues, string[] expectedUnprocessedArgs)
            => base.Can_Parse_Arguments(prototype, description, requiredOrOptional, args, expectedValues
                , expectedUnprocessedArgs);
#pragma warning restore xUnit1008

    }
}
