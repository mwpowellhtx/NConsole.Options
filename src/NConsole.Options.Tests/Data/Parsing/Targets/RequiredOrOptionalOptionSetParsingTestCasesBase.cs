using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Data.Parsing.Targets
{
    using static TestFixtureBase;

    internal abstract class RequiredOrOptionalOptionSetParsingTestCasesBase<T> : OptionSetParsingTestCasesBase
    {
        protected abstract IEnumerable<string> RenderValue(T value);

        protected abstract IEnumerable<T> NominalValues { get; }

        protected delegate IEnumerable<string> RenderPrototypeCasesDelegate<in TTarget>(
            string prefix, string prototypeName, char? requiredOrOptional, TTarget value);

        // ReSharper disable once UnusedMember.Global
        /// <summary>
        /// Override in order to furnish the Default <typeparamref name="T"/> oriented
        /// Case Rendering Callbacks.
        /// </summary>
        protected virtual IEnumerable<RenderPrototypeCasesDelegate<T>> RenderCaseCallbacks
        {
            get
            {
                IEnumerable<string> RenderBaseCase(string prefix, string prototypeName, char? requiredOrOptional, T value)
                {
                    yield return $"{prefix}{prototypeName}{RenderRequiredOrOptional(requiredOrOptional)}{RenderValue(value).Single()}";
                }

                IEnumerable<string> RenderExtendedCase(string prefix, string prototypeName, char? requiredOrOptional, T value)
                {
                    yield return $"{prefix}{prototypeName}";
                    yield return $"{RenderValue(value).Single()}";
                }

                yield return RenderBaseCase;
                yield return RenderExtendedCase;
            }
        }

        protected virtual IEnumerable<object[]> RenderAllArguments(IEnumerable<string> prototypeNames
            , string prefix, string currentPrototype, char? requiredOrOptional, T value)
        {
            // ReSharper disable PossibleMultipleEnumeration
            var expectedNames = prototypeNames.Where(x => DoesPrototypeContainName(currentPrototype, x)).ToArray();
            var unexpectedNames = prototypeNames.Where(x => !DoesPrototypeContainName(currentPrototype, x)).ToArray();

            foreach (var callback in RenderCaseCallbacks)
            {
                var args = prototypeNames.SelectMany(p => callback(prefix, p, requiredOrOptional, value)).ToArray();
                // ReSharper disable once ImplicitlyCapturedClosure
                var expectedValues = expectedNames.Select(_ => value).ToArray();
                var unprocessedArgs = unexpectedNames.SelectMany(p => callback(prefix, p, requiredOrOptional, value)).ToArray();

                yield return GetRangeArray<object>(args, expectedValues, unprocessedArgs);
            }
            // ReSharper restore PossibleMultipleEnumeration
        }

        protected virtual IEnumerable<object[]> RenderCases(string prefix, string currentPrototype, char? requiredOrOptional, IEnumerable<T> values)
        {
            var prototypeNames = PrototypeNames.ToArray();

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var value in values)
            {
                foreach (var cases in RenderAllArguments(prototypeNames, prefix, currentPrototype, requiredOrOptional, value))
                {
                    yield return cases;
                }
            }
        }

        private IEnumerable<object[]> _rootCases;

        private IEnumerable<object[]> RootCases
            => _rootCases ?? (_rootCases = MergeCases(
                   base.Cases
                   , RequiredOrOptionalCases.Where(x => x.HasValue).Select(x => (object) x)
               ));

        private IEnumerable<object[]> _privateCases;

        /// <summary>
        /// Gets the Root parameters concatenated with the calculated ones.
        /// </summary>
        /// <inheritdoc />
        protected override IEnumerable<object[]> Cases
        {
            get
            {
                IEnumerable<object[]> GetAll()
                {
                    // TODO: TBD: this tree of test cases is potentially, borderline combinatorial in nature...
                    // TODO: TBD: potentially identifying more values than just these... things like min, max, zero, etc
                    var values = NominalValues.ToArray();

                    foreach (var root in RootCases.Select(x => new
                        {
                            Prototype = (string) x[0],
                            Description = (string) x[1],
                            RequiredOrOptional = ((char?) x[2]).Value
                        })
                    )
                    {
                        foreach (var derived in UnbundledArgumentPrefixes.SelectMany(x =>
                                RenderCases(x, root.Prototype, root.RequiredOrOptional, values))
                            /*.Concat(BundledArgumentPrefixes.SelectMany(x =>
                                RenderCases(x, root.Prototype, root.RequiredOrOptional, values))
                            )*/)
                        {
                            //                                             prototype,      description,      requiredOrOptional
                            yield return GetRange<object>(root.Prototype, root.Description, root.RequiredOrOptional)
                                // Followed by: args, expectedValues, expectedUnprocessed
                                .Concat(derived).ToArray();
                        }
                    }
                }

                return _privateCases ?? (_privateCases = GetAll().ToArray());
            }
        }
    }
}
