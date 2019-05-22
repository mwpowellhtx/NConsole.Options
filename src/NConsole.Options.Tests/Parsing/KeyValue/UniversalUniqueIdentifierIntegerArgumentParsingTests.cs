using System;
using System.Collections.Generic;

namespace NConsole.Options.Parsing.KeyValue
{
    using Data.Parsing.KeyValue;
    using Xunit;
    using Xunit.Abstractions;

    public class UniversalUniqueIdentifierIntegerArgumentParsingTests : KeyValueArgumentParsingTestFixtureBase<Guid, int>
    {
        public UniversalUniqueIdentifierIntegerArgumentParsingTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

#pragma warning disable xUnit1008
        [ClassData(typeof(UniversalUniqueIdentifierIntegerOptionSetParsingTestCases))]
        public override void Can_Parse_Arguments(string prototype, string description, char requiredOrOptional
            , string[] args, KeyValuePair<Guid, int>[] expectedValues, string[] expectedUnprocessedArgs)
            => base.Can_Parse_Arguments(prototype, description, requiredOrOptional, args, expectedValues
                , expectedUnprocessedArgs);
#pragma warning restore xUnit1008

    }
}
