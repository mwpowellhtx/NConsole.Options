using System;

namespace NConsole.Options.Parsing.Targets
{
    using Data.Parsing.Targets;
    using Xunit;
    using Xunit.Abstractions;

    /// <summary>
    /// See comments concerning <see cref="EnumerationOptionSetParsingTestCases"/>
    /// for notes concerning nominal <see cref="StringComparison"/> ranges.
    /// </summary>
    public class EnumerationArgumentParsingTests : TargetArgumentParsingTestFixtureBase<StringComparison>
    {
        public EnumerationArgumentParsingTests(ITestOutputHelper outputHelper)
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
        [ClassData(typeof(EnumerationOptionSetParsingTestCases))]
        public override void Can_Parse_Arguments(string prototype, string description, char requiredOrOptional
            , string[] args, StringComparison[] expectedValues, string[] unprocessedArgs)
        {
            Callback = ParsedValues.Add;

            base.Can_Parse_Arguments(prototype, description, requiredOrOptional
                , args, expectedValues, unprocessedArgs);
        }
#pragma warning restore xUnit1008

    }
}
