using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Parsing.Simple
{
    using Registration.Simple;
    using Xunit;
    using Xunit.Abstractions;

    public abstract class SimpleArgumentParsingTestFixtureBase : SimpleOptionRegistrationTestFixtureBase
    {
        // ReSharper disable once RedundantEmptyObjectOrCollectionInitializer
        protected ICollection<bool> OptionsVisited { get; } = new List<bool> { };

        private void VerifyOptionsVisited(IEnumerable<bool> expected, IEnumerable<bool> actual)
            => actual.AssertNotNull().AssertEqual(expected.AssertNotNull());

        protected SimpleArgumentParsingTestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

#pragma warning disable xUnit1003
        /// <summary>
        /// Verifies that the <see cref="OptionSet"/> Can Parse the <paramref name="args"/>.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <param name="args"></param>
        /// <param name="expectedValues">Simply reflects whether we encountered
        /// the expected <see cref="Option"/> visitation during parsing.</param>
        /// <param name="expectedUnprocessedArgs"></param>
        [Theory]
        public virtual void Can_Parse_Arguments(string prototype, string description
            , string[] args, bool[] expectedValues, string[] expectedUnprocessedArgs
        )
        {
            OptionsVisited.AssertNotNull();

            // The Option Registration verifies Prototype, Description, etc.
            args.AssertNotNull().AssertNotEmpty();
            expectedValues.AssertNotNull();
            expectedUnprocessedArgs.AssertNotNull();

            // Clears the Parsed Values prior to the Next Parsing Attempt.
            void VerifyParsingResults(OptionSet options)
            {
                OptionsVisited.Clear();
                var actualUnprocessedArgs = options.Parse(args).AssertNotNull();
                OptionsVisited.AssertEqual(expectedValues);
                actualUnprocessedArgs.AssertNotNull().ToArray().AssertEqual(expectedUnprocessedArgs);
            }

            VerifyParsingResults(Register(prototype));
            VerifyParsingResults(Register(prototype, description));
        }
#pragma warning restore xUnit1003

    }
}
