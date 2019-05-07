namespace NConsole.Options
{
    public class OptionContext
    {
        public OptionValueCollection OptionValues { get; }

        public Option Option { get; set; }

        public string OptionName { get; set; }

        public int OptionIndex { get; set; }

        public OptionSet OptionSet { get; set; }

        public OptionContext(OptionSet optionSet)
        {
            OptionSet = optionSet;
            OptionValues = new OptionValueCollection(this);
        }
    }
}
