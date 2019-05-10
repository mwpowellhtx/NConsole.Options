using System;
using System.Collections.Generic;

namespace NConsole.Options
{
    using Xunit.Abstractions;
    using static Characters;
    using static OptionValueType;

    public abstract class OptionRegistrationTestFixtureBase : TestFixtureBase<OptionSet>
    {
        protected OptionRegistrationTestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        protected static bool RequiredOrOptionalPredicate(char? x, char? y) => x == y;

        protected static IEnumerable<char?> RequiredOrOptionalRange => GetRange<char?>(null, Equal, Colon);

        protected static bool TryDressPrototype(ref string prototype, char? requiredOrOptional, out OptionValueType expectedType)
        {
            var tried = false;

            expectedType = None;

            string Dress(string p) => $"{p}{requiredOrOptional.Value}";

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (requiredOrOptional)
            {
                case Equal:
                    prototype = Dress(prototype);
                    expectedType = Required;
                    return true;

                case Colon:
                    prototype = Dress(prototype);
                    expectedType = Optional;
                    return true;

                case null:
                    return true;
            }

            return false;
        }

        protected static string RenderRequiredOrOptional(char? x) => x == null ? "null" : $"{x}";
    }

    public abstract class OptionRegistrationTestFixtureBase<TCallback> : OptionRegistrationTestFixtureBase
        where TCallback : Delegate
    {
        protected TCallback Callback { get; }

        protected OptionRegistrationTestFixtureBase(ITestOutputHelper outputHelper, TCallback callback)
            : base(outputHelper)
        {
            Callback = callback;
        }

        protected abstract OptionSet Add(OptionSet options, string prototype, TCallback callback);

        protected abstract OptionSet Add(OptionSet options, string prototype, string description, TCallback callback);
    }
}
