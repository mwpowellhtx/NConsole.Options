using System.Linq;
using System.Reflection;

namespace NConsole.Options
{
    using Xunit;
    using Xunit.Abstractions;
    using static BindingFlags;

    public class OptionTests : TestFixtureBase<Option>
    {
        public OptionTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Fact]
        public void Type_Is_Public()
        {
            Assert.True(FixtureType.IsPublic);
        }

        [Fact]
        public void Type_Is_Abstract()
        {
            Assert.True(FixtureType.IsAbstract);
        }

        // ReSharper disable IdentifierTypo
        [Fact]
        public void Ctors_Are_Protected()
        {
            const string prototype = nameof(prototype);
            const string description = nameof(description);

            // IL Family is the reflection of C# Protected
            var ctors = FixtureType.GetConstructors(NonPublic | Instance);

            Assert.Collection(ctors.Select(ctor => ctor.IsFamily), Assert.True);

            Assert.Collection(ctors
                , ctor => VerifyParameters(
                    ctor.GetParameters()
                    , p => VerifyParameter<string>(p, prototype)
                    , p => VerifyParameter<string>(p, description)
                )
            );
        }
        // ReSharper restore IdentifierTypo
    }
}
