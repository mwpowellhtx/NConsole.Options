using System;
using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Data.Parsing.Targets
{
    using static TestFixtureBase;

    internal class IntegerOptionSetParsingTestCases : RequiredOrOptionalOptionSetParsingTestCasesBase
    {
        // ReSharper disable once IdentifierTypo
        protected virtual string RenderOneWordUnbunbledArgument(string prefix, string prototype
            , char requiredOrOptional, int value)
            => $"{prefix}{prototype}{requiredOrOptional}{value}";

        protected virtual string RenderTwoWordKeyPhrase(string prefix, string prototype) => $"{prefix}{prototype}";

        protected virtual string RenderTwoWordValuePhrase(int value) => $"{value}";

        // TODO: TBD: could these potentially be base class assets?
        // TODO: TBD: potentially much of it, leaving room for virtual rendering opportunities...
        private IEnumerable<object[]> GetOneWordUnbundledCases(string currentPrototype, char requiredOrOptional, params int[] values)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var prefix in UnbundledArgumentPrefixes)
            {
                foreach (var value in values)
                {
                    var candidates = PrototypeNames.Select(x => new
                    {
                        Argument = RenderOneWordUnbunbledArgument(prefix, x, requiredOrOptional, value),
                        IsExpected = currentPrototype.Contains(x),
                        ExpectedValue = value
                    }).ToArray();

                    yield return GetRangeArray<object>(
                        candidates.Select(x => x.Argument).ToArray() // args
                        , candidates.Where(x => x.IsExpected).Select(x => x.ExpectedValue).ToArray() // expectedValues
                        , candidates.Where(x => !x.IsExpected).Select(x => x.Argument).ToArray() // unprocessedArgs
                    );
                }
            }
        }

        private IEnumerable<object[]> GetTwoWordCases(string currentPrototype, params int[] values)
        {
            foreach (var prefix in UnbundledArgumentPrefixes)
            {
                foreach (var value in values)
                {
                    // ReSharper disable once RedundantEmptyObjectOrCollectionInitializer
                    var expectedValues = new List<int> { };

                    bool TryAddExpectedValue(Func<bool> predicate)
                    {
                        // ReSharper disable once InvertIf
                        if (predicate())
                        {
                            expectedValues.Add(value);
                            return true;
                        }

                        return false;
                    }

                    var candidates = PrototypeNames.SelectMany(x =>
                    {
                        // We have to do a little more bookkeeping in these cases.
                        var isExpected = TryAddExpectedValue(() => currentPrototype.Contains(x));

                        return GetRange(
                            RenderTwoWordKeyPhrase(prefix, x)
                            , RenderTwoWordValuePhrase(value)
                        ).Select(
                            y => new {Argument = y, IsExpected = isExpected}
                        );
                    }).ToArray();

                    yield return GetRange<object>(
                        candidates.Select(x => x.Argument).ToArray() // args
                        , expectedValues.ToArray() // expectedValues
                        , candidates.Where(x => !x.IsExpected).Select(x => x.Argument).ToArray() // unprocessedArgs
                    ).ToArray();
                }
            }
        }

        private static IEnumerable<object[]> _privateCases;

        /// <summary>
        /// Gets the Root parameters concatenated with the calculated ones.
        /// </summary>
        /// <see cref="GetOneWordUnbundledCases"/>
        /// <see cref="GetTwoWordCases"/>
        /// <inheritdoc />
        protected override IEnumerable<object[]> Cases
        {
            get
            {
                IEnumerable<object[]> GetAll()
                {
                    // TODO: TBD: this tree of test cases is potentially, borderline combinatorial in nature...
                    // TODO: TBD: potentially identifying more values than just these... things like min, max, zero, etc
                    var values = GetRangeArray(3);

                    foreach (var root in base.Cases.Select(x => new
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
