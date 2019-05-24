using System.Collections.Generic;

namespace NConsole.Options
{
    /// <summary>
    /// Represents a facade of <see cref="Option"/> for use with internal pattern matching.
    /// </summary>
    public interface IOption
    {
        /// <summary>
        /// Gets the Prototype.
        /// </summary>
        string Prototype { get; }

        /// <summary>
        /// Gets the Description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the ValueType.
        /// </summary>
        /// <see cref="OptionValueType"/>
        OptionValueType? ValueType { get; }

        /// <summary>
        /// Gets the Names.
        /// </summary>
        IReadOnlyList<string> Names { get; }

        /// <summary>
        /// Gets the Separators.
        /// </summary>
        IReadOnlyList<char> Separators { get; }
    }
}
