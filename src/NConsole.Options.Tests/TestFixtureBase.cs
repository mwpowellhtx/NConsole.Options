using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NConsole.Options
{
    using Xunit;
    using Xunit.Abstractions;

    public abstract class TestFixtureBase : IDisposable
    {
        protected virtual OptionSet RegisterOptions(Action<OptionSet> initializer = null)
        {
            // ReSharper disable once RedundantEmptyObjectOrCollectionInitializer
            var options = new OptionSet { };
            initializer?.Invoke(options);
            return options;
        }

        protected static void VerifyOption<T>(Option option, string prototype, OptionValueType? valueType)
            where T : Option
            => VerifyOption<T>(option, prototype, null, valueType);

        protected static void VerifyOption<T>(Option option, string prototype, string description, OptionValueType? valueType)
            where T : Option
        {
            var x = option.AssertNotNull().AssertIsType<T>();
            x.Prototype.AssertEqual(prototype);
            x.Description.AssertEqual(description);
            x.ValueType.AssertEqual(valueType);
        }

        protected ITestOutputHelper OutputHelper { get; }

        protected TestFixtureBase(ITestOutputHelper outputHelper)
        {
            OutputHelper = outputHelper;
        }

        protected internal static IEnumerable<T> GetRange<T>(params T[] values)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var value in values)
            {
                yield return value;
            }
        }

        protected internal static T[] GetRangeArray<T>(params T[] values) => GetRange(values).ToArray();

        protected internal static IEnumerable<T> GetRepeatedRange<T>(T value, int count)
        {
            while (count-- > 0)
            {
                yield return value;
            }
        }

        protected static void VerifyParameter<T>(ParameterInfo parameter, string expectedName)
        {
            Assert.NotNull(parameter);
            Assert.Equal(typeof(T), parameter.ParameterType);
            Assert.Equal(expectedName, parameter.Name);
        }

        protected static void VerifyParameters(IEnumerable<ParameterInfo> parameters
            , params Action<ParameterInfo>[] parameterInspectors)
            => Assert.Collection(parameters, parameterInspectors);

        //protected static void VerifyTypes(IEnumerable<Type> types
        //    , params Action<Type>[] typeInspectors)
        //    => Assert.Collection(types, typeInspectors);

        protected static OptionContext VerifyOptionContext(OptionContext context, Option option
            , string prototype, int? optionIndex, Action<OptionContext> verify = null
            , Action<OptionSet> verifySet = null, Action<OptionParameterCollection> verifyCollection = null)
        {
            Action<object> GetNullOrNotNull()
            {
                var notNull = (Action<object>)Assert.NotNull;
                return option == null ? Assert.Null : notNull;
            }

            GetNullOrNotNull().Invoke(context?.Option);

            if (context?.Option != null)
            {
                Assert.Equal(prototype, context.Option.Prototype);

                if (!ReferenceEquals(option, context.Option))
                {
                    Assert.Equal(option.Names, context.Option.Names);

                    if (optionIndex.HasValue)
                    {
                        Assert.Equal(optionIndex.Value, context.OptionIndex);
                    }
                }
            }

            verify?.Invoke(context);
            verifySet?.Invoke(context?.Set);
            verifyCollection?.Invoke(context?.Parameters);

            return context;
        }

        // TODO: TBD: this one is necessary? how much "context" can be exposed for verification?
        protected void VerifyContext(OptionSet options, Action<OptionContext> callback)
        {
            Assert.NotNull(callback);

            var context = new OptionContext(options);

            callback(context);
        }

        protected bool IsDisposed { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                Dispose(true);
            }

            IsDisposed = true;
        }
    }

    public abstract class TestFixtureBase<T> : TestFixtureBase
    {
        protected Type FixtureType { get; } = typeof(T);

        protected TestFixtureBase(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }
    }
}
