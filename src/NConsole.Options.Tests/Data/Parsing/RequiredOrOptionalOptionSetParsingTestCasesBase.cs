using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options.Data.Parsing
{
    using static TestFixtureBase;

    internal abstract class RequiredOrOptionalOptionSetParsingTestCasesBase<T> : OptionSetParsingTestCasesBase
    {
        protected abstract IEnumerable<string> RenderValue(T value);

        protected abstract IEnumerable<T> GetNominalValueRange();

        protected delegate IEnumerable<string> RenderPrototypeCasesDelegate<in TTarget>(
            string prefix, string prototypeName, char? requiredOrOptional, TTarget value);

        /// <summary>
        /// Override in order to furnish the Default <typeparamref name="T"/> oriented
        /// Case Rendering Callbacks.
        /// </summary>
        protected abstract IEnumerable<RenderPrototypeCasesDelegate<T>> RenderCaseCallbacks { get; }

        protected abstract IEnumerable<object[]> RenderAllArguments(IEnumerable<string> prototypeNames
            , string prefix, string currentPrototype, char? requiredOrOptional, T value);

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
                    var values = GetNominalValueRange().ToArray();

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
