using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Cases
{
    using Xunit;
    using Xunit.Abstractions;

    public abstract class RequiredOrOptionalOptionSetParsingTestCaseTestFixtureBase<T, TRequiredOrOptional> : TestFixtureBase
    {
        protected RequiredOrOptionalOptionSetParsingTestCaseTestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        protected abstract IEnumerable<object[]> Cases { get; }

        private static void VerifyEachCase(IEnumerable<object> values)
        {
            // ReSharper disable PossibleMultipleEnumeration
            values.AssertNotNull().AssertNotEmpty();

            values.AssertCollection(
                x => x.AssertIsType<string>()
                , x => x.AssertIsType<string>()
                , x => x.AssertIsType<TRequiredOrOptional>()
                , x => x.AssertIsType<string[]>()
                , x => x.AssertIsType<T[]>()
                , x => x.AssertIsType<string[]>()
            );

            // ReSharper restore PossibleMultipleEnumeration
        }

        [Fact]
        public virtual void Is_Not_Null() => Cases.AssertNotNull();

        [Fact]
        public virtual void Is_Not_Empty() => Cases.AssertNotEmpty();

        [Fact]
        public virtual void Verify_Test_Cases() => Cases.ToArray().ForEach(VerifyEachCase);
    }
}
