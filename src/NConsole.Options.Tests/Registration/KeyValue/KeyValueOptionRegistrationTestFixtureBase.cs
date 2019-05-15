namespace NConsole.Options.Registration.KeyValue
{
    using Data.Registration;
    using Xunit;
    using Xunit.Abstractions;

    public abstract class KeyValueOptionRegistrationTestFixtureBase<TKey, TValue>
        : OptionRegistrationTestFixtureBase<OptionCallback<TKey, TValue>>
    {
        protected KeyValueOptionRegistrationTestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper, (_, __) => { })
        {
        }

        protected override OptionSet Add(OptionSet options, string prototype
            , OptionCallback<TKey, TValue> callback)
            => options.Add(prototype, callback);

        protected override OptionSet Add(OptionSet options, string prototype
            , string description, OptionCallback<TKey, TValue> callback)
            => options.Add(prototype, description, callback);

        /// <summary>
        /// Registration serves to verify Option Registration as well as Argument Parsing.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="requiredOrOptional"></param>
        /// <returns></returns>
        protected OptionSet Register(string prototype, char? requiredOrOptional)
        {
            var p = prototype.AssertNotNull().AssertNotEmpty();
            var roo = requiredOrOptional.AssertContainedBy(RequiredOrOptionalRange, RequiredOrOptionalPredicate);

            OutputHelper.WriteLine(
                $"Registering Option with: Prototype=`{p}'"
                + $", RequiredOrOptional=`{RenderRequiredOrOptional(roo)}'"
            );

            TryDressPrototype(ref p, roo, out var expectedType).AssertTrue();

            return RegisterOptions(
                o => Add(o, p, Callback)
                    .AssertNotNull()
                    .AssertCollection(
                        x => VerifyOption<KeyValueActionOption<TKey, TValue>>(x, p, expectedType)
                    )
            );
        }

        /// <summary>
        /// Registration serves to verify Option Registration as well as Argument Parsing.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <param name="requiredOrOptional"></param>
        /// <returns></returns>
        protected OptionSet Register(string prototype, string description, char? requiredOrOptional)
        {
            var p = prototype.AssertNotNull().AssertNotEmpty();
            var d = description.AssertNotNull().AssertNotEmpty();
            var roo = requiredOrOptional.AssertContainedBy(RequiredOrOptionalRange, RequiredOrOptionalPredicate);

            OutputHelper.WriteLine(
                $"Registering Option with: Prototype=`{p}', Description=`{d}'"
                + $", RequiredOrOptional=`{RenderRequiredOrOptional(roo)}'"
            );

            TryDressPrototype(ref p, roo, out var expectedType).AssertTrue();

            return RegisterOptions(
                o => Add(o, p, d, Callback)
                    .AssertNotNull()
                    .AssertCollection(
                        x => VerifyOption<KeyValueActionOption<TKey, TValue>>(x, p, d, expectedType)
                    )
            );
        }

        /// <summary>
        /// The basic paths to adding <see cref="KeyValueActionOption{TKey,TValue}"/> involve
        /// <see cref="string"/> key and <see cref="string"/> value. However, this may be extended
        /// to virtually any key, value types in which there is a conferred type conversion.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <param name="requiredOrOptional"></param>
        [Theory, ClassData(typeof(RequiredOrOptionalOptionSetRegistrationTestCases))]
        public void Can_Add_Key_Value_Option(string prototype, string description, char? requiredOrOptional)
        {
            Register(prototype, requiredOrOptional);
            Register(prototype, description, requiredOrOptional);
        }
    }
}
