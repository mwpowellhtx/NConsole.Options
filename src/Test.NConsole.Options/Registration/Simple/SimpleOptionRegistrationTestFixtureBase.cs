using System.Collections.Generic;

namespace NConsole.Options.Registration.Simple
{
    using Data.Registration;
    using Xunit;
    using Xunit.Abstractions;

    public abstract class SimpleOptionRegistrationTestFixtureBase : OptionRegistrationTestFixtureBase<OptionCallback>
    {
        protected SimpleOptionRegistrationTestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
            Callback = () => { };
        }

        protected override OptionSet Add(OptionSet options, string prototype, OptionCallback callback)
            => options.Add(prototype, callback);

        protected override OptionSet Add(OptionSet options, string prototype, string description, OptionCallback callback)
            => options.Add(prototype, description, callback);

        protected override IEnumerable<Option> VerifyOptions(IEnumerable<Option> options, string prototype, OptionValueType? expectedType)
            => options.AssertCollection(
                o => VerifyOption<SimpleActionOption>(o, prototype, expectedType)
            );

        protected override IEnumerable<Option> VerifyOptions(IEnumerable<Option> options, string prototype, string description, OptionValueType? expectedType)
            => options.AssertCollection(
                o => VerifyOption<SimpleActionOption>(o, prototype, description, expectedType)
            );

        // TODO: TBD: I know what we were driving at with the `Register' approach, however, I think it needs to be revisited just a bit...
        /// <summary>
        /// Registration serves to verify Option Registration as well as Argument Parsing.
        /// </summary>
        /// <param name="prototype"></param>
        /// <returns></returns>
        protected OptionSet Register(string prototype)
        {
            var p = prototype.AssertNotNull().AssertNotEmpty();

            OutputHelper.WriteLine($"Registering OptionSet with: Prototype=`{p}'");

            TryDressPrototype(ref p, null, out var expectedType).AssertTrue();

            return RegisterOptions(
                o => Add(o, p, Callback)
                    .AssertNotNull()
                    .AssertCollection(
                        x => VerifyOption<SimpleActionOption>(x, p, expectedType)
                    )
            );
        }

        /// <summary>
        /// Registration serves to verify Option Registration as well as Argument Parsing.
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        protected OptionSet Register(string prototype, string description)
        {
            var p = prototype.AssertNotNull().AssertNotEmpty();
            var d = description.AssertNotNull().AssertNotEmpty();

            OutputHelper.WriteLine($"Registering OptionSet with: Prototype=`{p}', Description=`{d}'");

            TryDressPrototype(ref p, null, out var expectedType).AssertTrue();

            return RegisterOptions(
                o => Add(o, p, d, Callback)
                    .AssertNotNull()
                    .AssertCollection(
                        x => VerifyOption<SimpleActionOption>(x, p, d, expectedType)
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
        [Theory
            , ClassData(typeof(OptionSetRegistrationTestCases))
            ]
        public void Can_Add_Option(string prototype, string description)
        {
            Register(prototype);
            Register(prototype, description);
        }
    }
}
