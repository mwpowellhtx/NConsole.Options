using System;
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

        protected SimpleArgumentParsingTestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

#pragma warning disable xUnit1003
        /// <summary>
        /// Verifies that the <see cref="OptionSet"/> Can Parse the <paramref name="args"/>.
        /// This also has the desirable side effect of verify when
        /// <see cref="UnprocessedRequiredOptionsException"/> is not thrown.
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

            Callback = () => OptionsVisited.Add(true);

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

#pragma warning disable xUnit1003
        /// <summary>
        /// Verifies whether <see cref="OptionSet"/> Did Throw On Unprocessed <see cref="Option"/>
        /// instances. There is no need to rinse and repeat this expectation for Targeted, nor for
        /// Key Value Paired argument parsing, as this level of thrown <see cref="Exception"/> is
        /// consistent regardless of the number of expected <see cref="Option"/> Parameters.
        /// </summary>
        /// <param name="prototypes"></param>
        /// <param name="args"></param>
        /// <param name="unprocessedPrototypes"></param>
        [Theory]
        public virtual void Did_Throw_On_Unprocessed_Options(string[] prototypes
            , string[] args, string[] unprocessedPrototypes)
        {
            prototypes.AssertNotNull().AssertNotEmpty();
            args.AssertNotNull();

            Callback = () => { };

            Action verify = () =>
            {
                // TODO: TBD: perhaps we also adapt the Registration method for this purposes...
                var options = new OptionSet {{prototypes[0], Callback}}.AssertNotNull().AssertNotEmpty();

                // TODO: TBD: potentially informs a recasting of the OptionSet registration approach...
                options.Count.AssertEqual(1);

                // We do not care what the Callback are, per se, in this case.
                for (var i = 1; i < prototypes.Length; ++i)
                {
                    options.Add(prototypes[i], Callback).Count.AssertEqual(i + 1);
                }

                options.Parse(args);
            };

            verify.AssertThrowsException<UnprocessedRequiredOptionsException>(ex =>
            {
                var unprocessedOptions = ex.AssertNotNull().UnprocessedOptions.AssertNotNull();

                unprocessedOptions.Count.AssertEqual(unprocessedPrototypes.Length);

                unprocessedOptions.OrderBy(x => x.Prototype)
                    .Select(x => x.Prototype).Zip(unprocessedPrototypes.OrderBy(y => y)
                        , (x, y) => x == y).ToList().ForEach(Assert.True);
            });
        }
#pragma warning restore xUnit1003

    }
}
