using System.Collections.Generic;

namespace NConsole.Options
{
    using Xunit.Abstractions;

    public abstract class OptionSetTestFixtureBase : TestFixtureBase<OptionSet>
    {
        // ReSharper disable once RedundantEmptyObjectOrCollectionInitializer
        protected IDictionary<string, object> OptionsVisited { get; }
            = new Dictionary<string, object> { };

        protected OptionSetTestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
