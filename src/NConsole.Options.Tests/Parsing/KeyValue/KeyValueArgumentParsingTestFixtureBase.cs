using System.Collections.Generic;

namespace NConsole.Options.Parsing.KeyValue
{
    using Targets;
    using Xunit;
    using Xunit.Abstractions;

    public abstract class KeyValueArgumentParsingTestFixtureBase<TKey, TValue>
        : OptionRegistrationTestFixtureBase<OptionCallback<TKey, TValue>>
    {
        // ReSharper disable once RedundantEmptyObjectOrCollectionInitializer
        protected virtual ICollection<KeyValuePair<TKey, TValue>> PairsVisited { get; }
            = new List<KeyValuePair<TKey, TValue>> { };

        // ReSharper disable UseDeconstructionOnParameter
        protected virtual void VerifyPairVisited(KeyValuePair<TKey, TValue> expected, KeyValuePair<TKey, TValue> actual)
        {
            void AssertEqual<T>(T x, T y) => y.AssertEqual(x);

            AssertEqual(expected.Key, actual.Key);
            AssertEqual(expected.Value, actual.Value);
        }
        // ReSharper disable UseDeconstructionOnParameter

        protected KeyValueArgumentParsingTestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
            Callback = (k, v) => PairsVisited.Add(KeyValuePair.Create(k, v));
        }

        //#pragma warning disable xUnit1008
        //        [ClassData(typeof(System.String))]
        //        public override void Can_Parse_Arguments(string prototype, string description, char requiredOrOptional
        //            , string[] args,KeyValuePair<TKey, TValue>[] expectedValues, string[] expectedUnprocessedArgs)
        //        {
        //            base.Can_Parse_Arguments(prototype, description, requiredOrOptional, args, expectedValues, expectedUnprocessedArgs);
        //        }
        //#pragma warning restore xUnit1008

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
            , string[] args, KeyValuePair<TKey, TValue>[] expectedValues, string[] expectedUnprocessedArgs
        )
        {
            PairsVisited.AssertNotNull();

            // The Option Registration verifies Prototype, Description, etc.
            args.AssertNotNull().AssertNotEmpty();
            expectedValues.AssertNotNull();
            expectedUnprocessedArgs.AssertNotNull();

            // Clears the Parsed Values prior to the Next Parsing Attempt.
            void VerifyParsingResults(OptionSet options)
            {
                PairsVisited.Clear();
                var actualUnprocessedArgs = options.Parse(args).AssertNotNull();
                Assert.Equal(expectedValues, PairsVisited);
                Assert.Equal(expectedUnprocessedArgs, actualUnprocessedArgs);
            }

            VerifyParsingResults(Register(prototype, requiredOrOptional));
            VerifyParsingResults(Register(prototype, description, requiredOrOptional));
        }
#pragma warning restore xUnit1003

    }
}
