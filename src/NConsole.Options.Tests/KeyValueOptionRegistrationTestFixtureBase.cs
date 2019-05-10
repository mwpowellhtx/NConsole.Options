namespace NConsole.Options
{
    using Xunit;
    using Xunit.Abstractions;

    public abstract class KeyValueOptionRegistrationTestFixtureBase<TKey, TValue> : OptionRegistrationTestFixtureBase<OptionCallback<TKey, TValue>>
    {
        protected KeyValueOptionRegistrationTestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper, (_, __) => { })
        {
        }

        protected override OptionSet Add(OptionSet options, string prototype, OptionCallback<TKey, TValue> callback)
            => options.Add(prototype, callback);

        protected override OptionSet Add(OptionSet options, string prototype, string description, OptionCallback<TKey, TValue> callback)
            => options.Add(prototype, description, callback);

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
            prototype.AssertNotNull().AssertNotEmpty();
            description.AssertNotNull().AssertNotEmpty();

            void Register(string p, string d, char? roo)
            {
                OutputHelper.WriteLine(
                    $"Registering Option with: Prototype=`{p}', Description=`{d}'"
                    + $", RequiredOrOptional=`{RenderRequiredOrOptional(roo)}'"
                );

                TryDressPrototype(ref p, roo, out var expectedType).AssertTrue();

                RegisterOptions(o => Add(o, p, Callback))
                    .AssertNotNull()
                    .AssertCollection(
                        o => VerifyOption<KeyValueActionOption<TKey, TValue>>(o, p, expectedType)
                    );

                RegisterOptions(o => Add(o, p, d, Callback))
                    .AssertNotNull()
                    .AssertCollection(
                        o => VerifyOption<KeyValueActionOption<TKey, TValue>>(o, p, d, expectedType)
                    );
            }

            Register(prototype, description
                , requiredOrOptional.AssertContainedBy(RequiredOrOptionalRange, RequiredOrOptionalPredicate)
            );
        }
    }
}