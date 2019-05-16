namespace NConsole.Options.Registration.Targets
{
    using Data.Registration;
    using Xunit;
    using Xunit.Abstractions;

    public abstract class TargetOptionRegistrationTestFixtureBase<TTarget>
        : OptionRegistrationTestFixtureBase<OptionCallback<TTarget>>
    {
        protected TargetOptionRegistrationTestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper, _ => { })
        {
        }

        protected override OptionSet Add(OptionSet options, string prototype
            , OptionCallback<TTarget> callback)
            => options.Add(prototype, callback);

        protected override OptionSet Add(OptionSet options, string prototype
            , string description, OptionCallback<TTarget> callback)
            => options.Add(prototype, description, callback);

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
                o => Add(o, p, Callback)
                    .AssertNotNull()
                    .AssertCollection(
                        x => VerifyOption<ActionOption<TTarget>>(x, p, expectedType)
                    )
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
                o => Add(o, p, d, Callback)
                    .AssertNotNull()
                    .AssertCollection(
                        x => VerifyOption<ActionOption<TTarget>>(x, p, d, expectedType)
                    )
            );
        }

        /// <summary>
        /// The basic paths to adding <see cref="ActionOption{TTarget}"/> involve
        /// <see cref="string"/>. However, this may be extended to virtually any type
        /// in which there is a conferred type conversion.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <param name="requiredOrOptional"></param>
        [Theory, ClassData(typeof(RequiredOrOptionalOptionSetRegistrationTestCases))]
        public void Can_Add_Option(string prototype, string description, char? requiredOrOptional)
        {
            Register(prototype, requiredOrOptional);
            Register(prototype, description, requiredOrOptional);
        }
    }
}
