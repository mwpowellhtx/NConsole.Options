using System.Collections.Generic;

namespace NConsole.Options.Parsing.Targets
{
    using Xunit;
    using Xunit.Abstractions;
    using NConsole.Options.Registration.Targets;

    public abstract class TargetArgumentParsingTestFixtureBase<TTarget>
        : TargetOptionRegistrationTestFixtureBase<TTarget>
    {
        protected TargetArgumentParsingTestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
            Callback = ParsedValues.Add;
        }

        // ReSharper disable once RedundantEmptyObjectOrCollectionInitializer
        private readonly IList<TTarget> _parsedValues = new List<TTarget> { };

        protected ICollection<TTarget> ParsedValues => _parsedValues.AssertNotNull();

        private void VerifyParsedValues(IEnumerable<TTarget> expectedValues)
            => Assert.Equal(expectedValues, ParsedValues);

#pragma warning disable xUnit1003
        /// <summary>
        /// Verifies that the <see cref="OptionSet"/> Can Parse the <paramref name="args"/>.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <param name="requiredOrOptional"></param>
        /// <param name="args"></param>
        /// <param name="expectedValues"></param>
        /// <param name="expectedUnprocessedArgs"></param>
        [Theory]
        public virtual void Can_Parse_Arguments(string prototype, string description, char requiredOrOptional
            , string[] args, TTarget[] expectedValues, string[] expectedUnprocessedArgs
        )
        {
            ParsedValues.AssertNotNull();

            // The Option Registration verifies Prototype, Description, etc.
            args.AssertNotNull().AssertNotEmpty();
            expectedValues.AssertNotNull();
            expectedUnprocessedArgs.AssertNotNull();

            // Clears the Parsed Values prior to the Next Parsing Attempt.
            void VerifyParsingResults(OptionSet options)
            {
                ParsedValues.Clear();
                var actualUnprocessedArgs = options.Parse(args).AssertNotNull();
                Assert.Equal(expectedValues, ParsedValues);
                Assert.Equal(expectedUnprocessedArgs, actualUnprocessedArgs);
            }

            VerifyParsingResults(Register(prototype, requiredOrOptional));
            VerifyParsingResults(Register(prototype, description, requiredOrOptional));
        }
#pragma warning restore xUnit1003

    }
}
