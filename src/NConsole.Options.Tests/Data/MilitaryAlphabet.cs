using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Data
{
    /// <summary>
    /// The Military Alphabet as defined from Alpha through Zebra.
    /// </summary>
    /// <inheritdoc />
    /// <see cref="!:http://en.wikipedia.org/wiki/NATO_phonetic_alphabet"/>
    internal class MilitaryAlphabet : IEnumerable<string>
    {
        private IEnumerable<string> Alphabet { get; }

        private MilitaryAlphabet()
        {
            Alphabet = TestFixtureBase.GetRange(
                "Alpha", "Bravo", "Charlie", "Delta"
                , "Echo", "Foxtrot", "Golf", "Hotel"
                // ReSharper disable once StringLiteralTypo
                , "India", "Juliett", "Kilo", "Lima"
                , "Mike", "November", "Oscar", "Papa"
                , "Quebec", "Romeo", "Sierra", "Tango"
                , "Uniform", "Victor", "Whiskey", "X-Ray"
                , "Yankee", "Zulu"
            ).ToArray();
        }

        public IEnumerator<string> GetEnumerator() => Alphabet.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        internal static MilitaryAlphabet Instance = new MilitaryAlphabet();
    }
}
