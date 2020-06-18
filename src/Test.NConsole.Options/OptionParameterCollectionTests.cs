using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NConsole.Options
{
    using Xunit;
    using Xunit.Abstractions;
    using static BindingFlags;

    public class OptionParameterCollectionTests : TestFixtureBase<OptionParameterCollection>
    {
        public OptionParameterCollectionTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Fact]
        public void Type_Is_Public()
        {
            Assert.True(FixtureType.IsPublic);
        }

        [Fact]
        public void Type_BaseType_Is_Object()
        {
            Assert.Equal(typeof(object), FixtureType.BaseType);
        }

        /// <summary>
        /// The Theorized parameter <paramref name="implementedInterfaceType"/> rolls up
        /// potentially other interfaces, however, we are only interested in a couple of
        /// top level, key interfaces.
        /// </summary>
        /// <param name="implementedInterfaceType"></param>
        [Theory
         , InlineData(typeof(IList))
         , InlineData(typeof(IList<string>))]
        public void FixtureType_Implements_Interface_Type(Type implementedInterfaceType)
        {
            var implementedInterfaces = FixtureType.GetInterfaces();

            Assert.Collection(
                implementedInterfaces.Where(type => type == implementedInterfaceType)
                , selectedType => Assert.Equal(implementedInterfaceType, selectedType)
            );
        }

        // ReSharper disable IdentifierTypo
        [Fact]
        public void Ctors_Are_Internal()
        {
            const string context = nameof(context);

            var ctors = FixtureType.GetConstructors(NonPublic | Instance);

            Assert.Collection(ctors
                , ctor => VerifyParameters(
                    ctor.GetParameters()
                    , p => VerifyParameter<OptionContext>(p, context))
            );
        }
        // ReSharper restore IdentifierTypo
    }
}
