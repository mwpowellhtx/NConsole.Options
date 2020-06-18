using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Data
{
    using static TestFixtureBase;

    internal abstract class TestCasesBase : IEnumerable<object[]>
    {
        /// <summary>
        /// 1
        /// </summary>
        protected const int One = 1;

        /// <summary>
        /// 3
        /// </summary>
        protected const int Nominal = 3;

        /// <summary>
        /// Gets the <see cref="MilitaryAlphabet.Instance"/>.
        /// </summary>
        /// <see cref="MilitaryAlphabet"/>
        /// <see cref="MilitaryAlphabet.Instance"/>
        protected static MilitaryAlphabet AlphabetInstance => MilitaryAlphabet.Instance;

        private static IEnumerable<string> _prototypeNames;

        /// <summary>
        /// Gets a sequence of <see cref="Nominal"/> <see cref="AlphabetInstance"/> Names.
        /// </summary>
        /// <see cref="Nominal"/>
        /// <see cref="AlphabetInstance"/>
        /// <see cref="MilitaryAlphabet"/>
        protected static IEnumerable<string> PrototypeNames
        {
            get
            {
                IEnumerable<string> GetAll(int count)
                    => AlphabetInstance.Take(count).Select(x => x.ToLower()).ToArray();

                return _prototypeNames ?? (_prototypeNames = GetAll(Nominal));
            }
        }

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
