using System.Collections.Generic;

namespace NConsole.Options.Registration.Targets
{
    using Data.Registration;
    using Xunit;
    using Xunit.Abstractions;

    public abstract class TargetOptionRegistrationTestFixtureBase<TTarget>
        : OptionRegistrationTestFixtureBase<OptionCallback<TTarget>>
    {
        protected TargetOptionRegistrationTestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
            Callback = _ => { };
        }

        protected override OptionSet Add(OptionSet options, string prototype, OptionCallback<TTarget> callback)
            => options.Add(prototype, callback);

        protected override OptionSet Add(OptionSet options, string prototype, string description, OptionCallback<TTarget> callback)
            => options.Add(prototype, description, callback);

        protected override IEnumerable<Option> VerifyOptions(IEnumerable<Option> options, string prototype, OptionValueType? expectedType)
            => options.AssertCollection(
                o => VerifyOption<ActionOption<TTarget>>(o, prototype, expectedType)
            );

        protected override IEnumerable<Option> VerifyOptions(IEnumerable<Option> options, string prototype, string description, OptionValueType? expectedType)
            => options.AssertCollection(
                o => VerifyOption<ActionOption<TTarget>>(o, prototype, description, expectedType)
            );

        /// <summary>
        /// The basic paths to adding <see cref="ActionOption{TTarget}"/> involve
        /// <see cref="string"/>. However, this may be extended to virtually any type
        /// in which there is a conferred type conversion.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <param name="requiredOrOptional"></param>
        [Theory
            , ClassData(typeof(RequiredOrOptionalOptionSetRegistrationTestCases))
            ]
        public void Can_Add_Option(string prototype, string description, char? requiredOrOptional)
        {
            Register(prototype, requiredOrOptional);
            Register(prototype, description, requiredOrOptional);
        }
    }
}
