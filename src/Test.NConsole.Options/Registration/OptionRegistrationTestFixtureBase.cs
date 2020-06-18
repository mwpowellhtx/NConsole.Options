using System;
using System.Collections.Generic;

namespace NConsole.Options
{
    using Xunit.Abstractions;
    using static Characters;
    using static String;
    using static OptionValueType;

    public abstract class OptionRegistrationTestFixtureBase : TestFixtureBase<OptionSet>
    {
        protected OptionRegistrationTestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        protected static bool RequiredOrOptionalPredicate(char? x, char? y) => x == y;

        protected static IEnumerable<char?> RequiredOrOptionalRange => GetRange<char?>(null, Equal, Colon);

        protected static bool TryDressPrototype(ref string prototype, char? requiredOrOptional
            , string separators, out OptionValueType? expectedType)
        {
            var tried = false;

            expectedType = null;

            string Dress(string p, string sep) => $"{p}{requiredOrOptional.Value}{(IsNullOrEmpty(sep) ? "" : sep)}";

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (requiredOrOptional)
            {
                case Equal:
                    prototype = Dress(prototype, separators);
                    expectedType = Required;
                    return true;

                case Colon:
                    prototype = Dress(prototype, separators);
                    expectedType = Optional;
                    return true;

                case null:
                    return true;
            }

            return false;
        }

        protected static bool TryDressPrototype(ref string prototype, char? requiredOrOptional, out OptionValueType? expectedType)
            => TryDressPrototype(ref prototype, requiredOrOptional, null, out expectedType);

        protected static string RenderRequiredOrOptional(char? x) => x == null ? "null" : $"{x}";
    }

    public abstract class OptionRegistrationTestFixtureBase<TCallback> : OptionRegistrationTestFixtureBase
        where TCallback : Delegate
    {
        private TCallback _callback;

        /// <summary>
        /// Gets or Sets the Callback, either via constructor argument or later on,
        /// such as during test verification.
        /// </summary>
        protected TCallback Callback
        {
            get => _callback.AssertNotNull();
            set => _callback = value.AssertNotNull();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputHelper"></param>
        protected OptionRegistrationTestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        protected abstract OptionSet Add(OptionSet options, string prototype, TCallback callback);

        protected abstract OptionSet Add(OptionSet options, string prototype, string description, TCallback callback);

        protected abstract IEnumerable<Option> VerifyOptions(IEnumerable<Option> options, string prototype, OptionValueType? expectedType);

        protected abstract IEnumerable<Option> VerifyOptions(IEnumerable<Option> options, string prototype, string description, OptionValueType? expectedType);

        /// <summary>
        /// Registration serves to verify Option Registration as well as Argument Parsing.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="requiredOrOptional"></param>
        /// <returns></returns>
        protected OptionSet Register(string prototype, char? requiredOrOptional = null)
        {
            var p = prototype.AssertNotNull().AssertNotEmpty();
            var roo = requiredOrOptional.AssertContainedBy(RequiredOrOptionalRange, RequiredOrOptionalPredicate);

            OutputHelper.WriteLine(
                $"Registering OptionSet with: Prototype=`{p}'"
                + $", RequiredOrOptional=`{RenderRequiredOrOptional(roo)}'"
            );

            TryDressPrototype(ref p, roo, out var expectedType).AssertTrue();

            return RegisterOptions(
                o => VerifyOptions(Add(o, p, Callback).AssertNotNull()
                    , p, expectedType)
            ).AssertNotNull();
        }

        /// <summary>
        /// Registration serves to verify Option Registration as well as Argument Parsing.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <param name="requiredOrOptional"></param>
        /// <returns></returns>
        protected OptionSet Register(string prototype, string description, char? requiredOrOptional = null)
        {
            var p = prototype.AssertNotNull().AssertNotEmpty();
            var d = description.AssertNotNull().AssertNotEmpty();
            var roo = requiredOrOptional.AssertContainedBy(RequiredOrOptionalRange, RequiredOrOptionalPredicate);

            OutputHelper.WriteLine(
                $"Registering OptionSet with: Prototype=`{p}', Description=`{d}'"
                + $", RequiredOrOptional=`{RenderRequiredOrOptional(roo)}'"
            );

            TryDressPrototype(ref p, roo, out var expectedType).AssertTrue();

            return RegisterOptions(
                o => VerifyOptions(Add(o, p, d, Callback).AssertNotNull()
                    , p, d, expectedType)
            ).AssertNotNull();
        }
    }
}
