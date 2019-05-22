//namespace NConsole.Options
//{
//    using Data.Registration;
//    using Xunit;
//    using Xunit.Abstractions;
//    using static OptionValueType;

//    // ReSharper disable once IdentifierTypo
//    public class ParameterlessOptionSetTests : OptionRegistrationTestFixtureBase<OptionCallback>
//    {
//        // ReSharper disable once IdentifierTypo
//        public ParameterlessOptionSetTests(ITestOutputHelper outputHelper)
//            : base(outputHelper, () => { })
//        {
//        }

//        protected override OptionSet Add(OptionSet options, string prototype, OptionCallback callback)
//            => options.Add(prototype, callback);

//        protected override OptionSet Add(OptionSet options, string prototype, string description, OptionCallback callback)
//            => options.Add(prototype, description, callback);

//        [Theory, ClassData(typeof(OptionSetRegistrationTestCases))]
//        public void Can_Add_Option(string prototype, string description)
//        {
//            prototype.AssertNotNull().AssertNotEmpty();
//            description.AssertNotNull().AssertNotEmpty();

//            const OptionValueType none = None;

//            void Register(string p, string d)
//            {
//                OutputHelper.WriteLine($"Registering OptionSet with: Prototype=`{p}', Description=`{d}'");

//                RegisterOptions(o => Add(o, p, Callback))
//                    .AssertNotNull()
//                    .AssertCollection(
//                        o => VerifyOption<SimpleActionOption>(o, p, none)
//                    );

//                RegisterOptions(o => Add(o, p, d, Callback))
//                    .AssertNotNull()
//                    .AssertCollection(
//                        o => VerifyOption<SimpleActionOption>(o, p, d, none)
//                    );
//            }

//            Register(prototype, description);
//        }
//    }
//}
