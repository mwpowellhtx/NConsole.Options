namespace NConsole.Options
{
    public class OptionContext
    {
        /// <summary>
        /// Gets the Parameters.
        /// </summary>
        internal OptionParameterCollection Parameters { get; }

        private Option _option;

        /// <summary>
        /// Gets the Option.
        /// </summary>
        public Option Option
        {
            get => _option;
            set
            {
                if (value == null || !ReferenceEquals(value, _option))
                {
                    OptionName = null;
                    Parameters.Clear();
                }

                _option = value;
            }
        }

        /// <summary>
        /// Gets whether the Context Has <see cref="Option"/>.
        /// </summary>
        internal bool HasOption => Option != null;

        /// <summary>
        /// Gets the Option Name.
        /// </summary>
        public string OptionName { get; internal set; }

        /// <summary>
        /// -1
        /// </summary>
        private const int DefaultOptionIndex = -1;

        /// <summary>
        /// Gets the OptionIndex. Defaults to <see cref="DefaultOptionIndex"/>.
        /// </summary>
        /// <see cref="DefaultOptionIndex"/>
        public int OptionIndex { get; internal set; } = DefaultOptionIndex;

        /// <summary>
        /// Gets the Set.
        /// </summary>
        public OptionSet Set { get; protected set; }

        internal OptionContext(OptionSet optionSet)
        {
            Set = optionSet;
            Parameters = new OptionParameterCollection(this);
        }

        /// <summary>
        /// Visits the <see cref="Option"/> given itself.
        /// </summary>
        /// <remarks><see cref="Option"/> should never be Null by this point. We guard against
        /// that ever being the case during the Argument Parsing Option Dispatch.</remarks>
        internal void Visit() => Option.Visit(this);
    }
}
