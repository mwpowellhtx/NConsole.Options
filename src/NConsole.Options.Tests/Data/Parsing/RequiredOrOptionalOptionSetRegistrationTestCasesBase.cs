using System;
using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Data.Parsing
{
    using Kingdom.Combinatorics.Combinatorials;
    using static TestFixtureBase;

    internal abstract class RequiredOrOptionalOptionSetParsingTestCasesBase<T> : OptionSetParsingTestCases
    {
        // ReSharper disable once IdentifierTypo
        protected abstract string RenderOneWordUnbunbledArgument(string prefix, string prototype
            , char requiredOrOptional, T value);

        protected static string RenderTwoWordKeyPhrase(string prefix, string prototype) => $"{prefix}{prototype}";

        protected abstract string RenderTwoWordValuePhrase(T value);

        protected virtual IEnumerable<object[]> GetOneWordUnbundledCases(string currentPrototype, char requiredOrOptional, params T[] values)
        {
            if (!values.Any())
            {
                yield break;
            }

            var combinations = UnbundledArgumentPrefixes.Combine(values);

            combinations.SilentOverflow = true;

            foreach (var current in combinations)
            {
                var prefix = (string) current[0];
                var value = (T) current[1];

                var candidates = PrototypeNames.Select(x => new
                {
                    Argument = RenderOneWordUnbunbledArgument(prefix, x, requiredOrOptional, value),
                    IsExpected = DoesPrototypeContainName(currentPrototype, x),
                    ExpectedValue = value
                }).ToArray();

                yield return GetRangeArray<object>(
                    candidates.Select(x => x.Argument).ToArray() // args
                    , candidates.Where(x => x.IsExpected).Select(x => x.ExpectedValue).ToArray() // expectedValues
                    , candidates.Where(x => !x.IsExpected).Select(x => x.Argument).ToArray() // unprocessedArgs
                );
            }
        }

        protected virtual IEnumerable<object[]> GetTwoWordCases(string currentPrototype, params T[] values)
        {
            if (!values.Any())
            {
                yield break;
            }

            var combinations = UnbundledArgumentPrefixes.Combine(values);

            combinations.SilentOverflow = true;

            bool TryAddExpectedValue(ICollection<T> expected, T value, Func<bool> predicate)
            {
                var previousCount = expected.Count;

                if (predicate())
                {
                    expected.Add(value);
                }

                return expected.Count > previousCount;
            }

            foreach (var current in combinations)
            {
                var prefix = (string) current[0];
                var value = (T) current[1];

                // ReSharper disable once RedundantEmptyObjectOrCollectionInitializer
                var expectedValues = new List<T> { };

                var candidates = PrototypeNames.SelectMany(x =>
                {
                    // We have to do a little more bookkeeping in these cases.
                    var isExpected = TryAddExpectedValue(expectedValues, value
                        , () => DoesPrototypeContainName(currentPrototype, x));

                    return GetRange(
                            RenderTwoWordKeyPhrase(prefix, x), RenderTwoWordValuePhrase(value))
                        .Select(y => new {Argument = y, IsExpected = isExpected});
                }).ToArray();

                yield return GetRange<object>(
                    candidates.Select(x => x.Argument).ToArray() // args
                    , expectedValues.ToArray() // expectedValues
                    , candidates.Where(x => !x.IsExpected).Select(x => x.Argument).ToArray() // unprocessedArgs
                ).ToArray();
            }
        }

        protected abstract IEnumerable<T> GetNominalValueRange();

        private IEnumerable<object[]> _rootCases;

        private IEnumerable<object[]> RootCases
            => _privateCases ?? (_privateCases = MergeCases(
                   base.Cases
                   , RequiredOrOptionalCases.Where(x => x.HasValue).Select(x => (object) x)
               ));

        private IEnumerable<object[]> _privateCases;

        /// <summary>
        /// Gets the Root parameters concatenated with the calculated ones.
        /// </summary>
        /// <inheritdoc />
        protected override IEnumerable<object[]> Cases
        {
            get
            {
                IEnumerable<object[]> GetAll()
                {
                    // TODO: TBD: this tree of test cases is potentially, borderline combinatorial in nature...
                    // TODO: TBD: potentially identifying more values than just these... things like min, max, zero, etc
                    var values = GetNominalValueRange().ToArray();

                    foreach (var root in RootCases.Select(x => new
                        {
                            Prototype = (string) x[0],
                            Description = (string) x[1],
                            RequiredOrOptional = ((char?) x[2]).Value
                        })
                    )
                    {
                        // ReSharper disable once PossibleInvalidOperationException
                        foreach (var derived in GetOneWordUnbundledCases(root.Prototype, root.RequiredOrOptional, values)
                            .Concat(GetTwoWordCases(root.Prototype, values)))
                        {
                            //                                             prototype,      description,      requiredOrOptional
                            yield return GetRange<object>(root.Prototype, root.Description, root.RequiredOrOptional)
                                // Followed by: args, expectedValues, expectedUnprocessed
                                .Concat(derived).ToArray();
                        }
                    }
                }

                return _privateCases ?? (_privateCases = GetAll().ToArray());
            }
        }
    }
}
