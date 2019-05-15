namespace NConsole.Options
{
    public class OptionContext
    {
        /// <summary>
        /// Gets the OptionValues.
        /// </summary>
        public OptionValueCollection OptionValues { get; }

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
                    OptionValues.Clear();
                }

                _option = value;
            }
        }

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
            OptionValues = new OptionValueCollection(this);
        }

        /// <summary>
        /// Visits the <see cref="Option"/> given itself.
        /// </summary>
        internal void Visit() => Option?.Visit(this);
    }
}
