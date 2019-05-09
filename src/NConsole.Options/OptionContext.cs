namespace NConsole.Options
{
    public class OptionContext
    {
        /// <summary>
        /// Gets the OptionValues.
        /// </summary>
        public OptionValueCollection OptionValues { get; }

        /// <summary>
        /// Gets the Option.
        /// </summary>
        public Option Option { get; set; }

        /// <summary>
        /// Gets the Option Name.
        /// </summary>
        public string OptionName { get; internal set; }

        /// <summary>
        /// Gets the OptionIndex.
        /// </summary>
        public int OptionIndex { get; internal set; }

        /// <summary>
        /// Gets the Set.
        /// </summary>
        public OptionSet Set { get; internal set; }

        internal OptionContext(OptionSet optionSet)
        {
            Set = optionSet;
            OptionValues = new OptionValueCollection(this);
        }
    }
}
