using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace NConsole.Options
{
    /// <summary>
    /// Represents an <see cref="Option"/> <see cref="Exception"/>.
    /// </summary>
    /// <inheritdoc />
    [Serializable]
    public class OptionException : Exception
    {
        /// <summary>
        /// Gets the OptionName.
        /// </summary>
        private string OptionName { get; }

        /// <inheritdoc />
        public OptionException()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="optionName"></param>
        /// <inheritdoc />
        public OptionException(string message, string optionName)
            : base(message)
        {
            OptionName = optionName;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="optionName"></param>
        /// <param name="innerException"></param>
        /// <inheritdoc />
        public OptionException(string message, string optionName, Exception innerException)
            : base(message, innerException)
        {
            OptionName = optionName;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        /// <inheritdoc />
        protected OptionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            OptionName = info.GetString(nameof(OptionName));
        }

        // TODO: TBD: see comments on LinkDemand, run with this? Or do we need a migration path from here?
        /// <inheritdoc />
        [SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = false)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(OptionName), OptionName);
        }
    }
}
