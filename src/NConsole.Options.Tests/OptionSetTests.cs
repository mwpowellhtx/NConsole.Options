using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options
{
    using Xunit;
    using Xunit.Abstractions;
    using static Characters;

    public class OptionSetTests : TestFixtureBase<OptionSet>
    {
        protected override OptionSet GetOptions()
        {
            /* It is probably extraneous, but it makes it clearer we also
             * want a Description out of the Option Names factory. */

            string GetOptionNames(ref int i, IEnumerable<string> names, out string description, int delta = 1)
            {
                i += delta;
                // ReSharper disable PossibleMultipleEnumeration
                Assert.True(i >= 0 && i < names.Count());
                var name = names.ElementAt(i);
                // ReSharper restore PossibleMultipleEnumeration
                Assert.NotNull(name);
                Assert.NotEmpty(name);
                description = $"{name} option.";
                name = name.ToLower();
                return $"{name[0]}{Pipe}{name}";
            }

            var index = -1;
            var alphabet = MilitaryAlphabet.Instance;

            string GetNextOptionNames(out string description)
            {
                var s = GetOptionNames(ref index, alphabet, out description);
                Assert.NotNull(description);
                Assert.NotEmpty(description);
                return s;
            }

            // ReSharper disable once InlineOutVariableDeclaration, InconsistentNaming
            string _description = null;

            // TODO: TBD: establish a base test fixture...
            // TODO: TBD: which derived fixtures exercise aspects of the add/parse feedback loop...
            // TODO: TBD: also exposing the "unprocessed" use case...

            // ReSharper disable CommentTypo
            return new OptionSet
            {
                // Should be a[lpha], b[ravo], c[harlie], d[elta].
                {GetNextOptionNames(out _description), _description, s => { }},
                {GetNextOptionNames(out _description), _description, (int x) => { }},
                {GetNextOptionNames(out _description), _description, (bool y) => { }},
                {GetNextOptionNames(out _description), _description, () => { }}
            };
            // ReSharper restore CommentTypo
        }

        public OptionSetTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
