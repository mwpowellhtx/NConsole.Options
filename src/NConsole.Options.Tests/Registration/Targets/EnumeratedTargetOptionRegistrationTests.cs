using System;
using System.Globalization;

namespace NConsole.Options.Registration.Targets
{
    using Xunit.Abstractions;

    public class CultureTypesTargetOptionRegistrationTests : TargetOptionRegistrationTestFixtureBase<CultureTypes>
    {
        public CultureTypesTargetOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }

    public class StringComparisonTargetOptionRegistrationTests : TargetOptionRegistrationTestFixtureBase<StringComparison>
    {
        public StringComparisonTargetOptionRegistrationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
