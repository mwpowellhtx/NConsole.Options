namespace NConsole.Options
{
    using Xunit;
    using Xunit.Abstractions;

    public abstract class TargetOptionRegistrationTestFixtureBase<TTarget> : OptionRegistrationTestFixtureBase<OptionCallback<TTarget>>
    {
        protected TargetOptionRegistrationTestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper, _ => { })
        {
        }

        protected override OptionSet Add(OptionSet options, string prototype, OptionCallback<TTarget> callback)
            => options.Add(prototype, callback);

        protected override OptionSet Add(OptionSet options, string prototype, string description, OptionCallback<TTarget> callback)
            => options.Add(prototype, description, callback);

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
            prototype.AssertNotNull().AssertNotEmpty();
            description.AssertNotNull().AssertNotEmpty();

            void Register(string p, string d, char? roo)
            {
                OutputHelper.WriteLine(
                    $"Registering OptionSet with: Prototype=`{p}', Description=`{d}'"
                    + $", RequiredOrOptional=`{RenderRequiredOrOptional(roo)}'"
                );

                TryDressPrototype(ref p, roo, out var expectedType).AssertTrue();

                RegisterOptions(o => Add(o, p, Callback))
                    .AssertNotNull()
                    .AssertCollection(
                        o => VerifyOption<ActionOption<TTarget>>(o, p, expectedType)
                    );

                RegisterOptions(o => Add(o, p, d, Callback))
                    .AssertNotNull()
                    .AssertCollection(
                        o => VerifyOption<ActionOption<TTarget>>(o, p, d, expectedType)
                    );
            }

            // We should be able to support the Nominal String-based use case.
            Register(prototype, description
                , requiredOrOptional.AssertContainedBy(RequiredOrOptionalRange, RequiredOrOptionalPredicate)
            );
        }
    }
}
