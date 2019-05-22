using System.Collections.Generic;

namespace NConsole.Options.Parsing.KeyValue
{
    using Xunit;
    using Xunit.Abstractions;

    public abstract class KeyValueArgumentParsingTestFixtureBase<TKey, TValue>
        : OptionRegistrationTestFixtureBase<OptionCallback<TKey, TValue>>
    {
        // ReSharper disable once RedundantEmptyObjectOrCollectionInitializer
        protected virtual ICollection<KeyValuePair<TKey, TValue>> PairsVisited { get; }
            = new List<KeyValuePair<TKey, TValue>> { };

        protected KeyValueArgumentParsingTestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
            Callback = (k, v) => PairsVisited.Add(KeyValuePair.Create(k, v));
        }

        protected override OptionSet Add(OptionSet options, string prototype, OptionCallback<TKey, TValue> callback)
            => options.Add(prototype, callback);

        protected override OptionSet Add(OptionSet options, string prototype, string description, OptionCallback<TKey, TValue> callback)
            => options.Add(prototype, description, callback);

        protected override IEnumerable<Option> VerifyOptions(IEnumerable<Option> options, string prototype, OptionValueType? expectedType)
            => options.AssertCollection(
                o => VerifyOption<KeyValueActionOption<TKey, TValue>>(o.AssertNotNull(), prototype, expectedType)
            ).AssertNotNull();

        protected override IEnumerable<Option> VerifyOptions(IEnumerable<Option> options, string prototype, string description, OptionValueType? expectedType)
            => options.AssertCollection(
                o => VerifyOption<KeyValueActionOption<TKey, TValue>>(o.AssertNotNull(), prototype, description, expectedType)
            ).AssertNotNull();

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
