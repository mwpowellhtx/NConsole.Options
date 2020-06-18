using System;

namespace NConsole.Options.Data
{
    [Flags]
    internal enum ArgumentRequest
    {
        /// <summary>
        /// 0
        /// </summary>
        None = 0,

        /// <summary>
        /// 1, or 1, or 1b
        /// </summary>
        Letter = 1,

        /// <summary>
        /// 1 &gt;&gt; 1, or 2, or 10b
        /// </summary>
        Full = 1 >> 1,

        /// <summary>
        /// 3, or 11b
        /// </summary>
        /// <see cref="Letter"/>
        /// <see cref="Full"/>
        Both = Letter | Full
    }
}
