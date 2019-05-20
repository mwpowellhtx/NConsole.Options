using System.Collections.Generic;

namespace NConsole.Options.Parsing.KeyValue
{
    using Registration.KeyValue;
    using Xunit;
    using Xunit.Abstractions;

    public abstract class KeyValueArgumentParsingTestFixtureBase<TKey, TValue, TEqualityComparer>
        : KeyValueOptionRegistrationTestFixtureBase<TKey, TValue>
        where TEqualityComparer : class, IEqualityComparer<IEnumerable<KeyValuePair<TKey, TValue>>>, new()
    {
        // ReSharper disable once RedundantEmptyObjectOrCollectionInitializer
        protected virtual ICollection<KeyValuePair<TKey, TValue>> PairsVisited { get; }
            = new List<KeyValuePair<TKey, TValue>> { };

        protected abstract class PairComparerBase : IEqualityComparer<IEnumerable<KeyValuePair<TKey, TValue>>>
        {
            public abstract bool Equals(IEnumerable<KeyValuePair<TKey, TValue>> x
                , IEnumerable<KeyValuePair<TKey, TValue>> y);

            public abstract int GetHashCode(IEnumerable<KeyValuePair<TKey, TValue>> obj);
        }

        protected virtual void VerifyPairsVisited(IEnumerable<KeyValuePair<TKey, TValue>> expected
            , IEnumerable<KeyValuePair<TKey, TValue>> actual)
        {
            // ReSharper disable once RedundantEmptyObjectOrCollectionInitializer
            actual.AssertEqual(expected, new TEqualityComparer { });
        }

        protected KeyValueArgumentParsingTestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

#pragma warning disable xUnit1003
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <param name="args"></param>
        /// <param name="requiredOrOptional"></param>
        /// <param name="separators"></param>
        /// <param name="expectedUnprocessedArgs"></param>
        [Theory]
        public virtual void Can_Parse_Arguments(string prototype, string description, string[] args
            , char? requiredOrOptional, string separators, KeyValuePair<TKey, TValue>[] expectedPairs
            , string[] expectedUnprocessedArgs
        )
        {
            PairsVisited.AssertNotNull().AssertEmpty();

            args.AssertNotNull().AssertNotEmpty();
            expectedPairs.AssertNotNull();
            expectedUnprocessedArgs.AssertNotNull();

            // Clears the Parsed Values prior to the Next Parsing Attempt.
            void VerifyParsingResults(OptionSet options)
            {
                PairsVisited.Clear();
                var actualUnprocessedArgs = options.Parse(args).AssertNotNull();
                // ReSharper disable once RedundantEmptyObjectOrCollectionInitializer
                PairsVisited.AssertEqual(expectedPairs, new TEqualityComparer { });
                actualUnprocessedArgs.AssertNotNull().ToArray().AssertEqual(expectedUnprocessedArgs);
            }

            VerifyParsingResults(Register(prototype, requiredOrOptional, separators));
            VerifyParsingResults(Register(prototype, description, requiredOrOptional, separators));
        }
#pragma warning restore xUnit1003

    }
}
