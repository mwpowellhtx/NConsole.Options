using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options
{
    using static TestFixtureBase;

    internal abstract class TestCasesBase : IEnumerable<object[]>
    {
        protected abstract IEnumerable<object[]> Cases { get; }

        public IEnumerator<object[]> GetEnumerator() => Cases.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        // ReSharper disable PossibleMultipleEnumeration, LoopCanBeConvertedToQuery
        protected static IEnumerable<object[]> MergeCases(IEnumerable<object[]> cases, IEnumerable<object> dimension)
        {
            foreach (var x in cases)
            {
                foreach (var y in dimension)
                {
                    yield return x.Concat(GetRange(y)).ToArray();
                }
            }
        }

        protected static IEnumerable<object[]> MergeCases(IEnumerable<object[]> cases, IEnumerable<object[]> dimensions)
        {
            foreach (var x in cases)
            {
                foreach (var y in dimensions)
                {
                    yield return x.Concat(y).ToArray();
                }
            }
        }
        // ReSharper restore PossibleMultipleEnumeration, LoopCanBeConvertedToQuery
    }
}
