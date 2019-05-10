using System;
using System.Globalization;

namespace NConsole.Options
{
    using Xunit.Abstractions;

    public class StringComparisonCultureTypesKeyValueOptionRegistrationTests : KeyValueOptionRegistrationTestFixtureBase<StringComparison, CultureTypes>
    {
        public StringComparisonCultureTypesKeyValueOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
