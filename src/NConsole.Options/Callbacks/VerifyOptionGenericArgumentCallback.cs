using System;

namespace NConsole.Options
{
    /// <summary>
    /// Verify the <paramref name="genericType"/> is what we think it is.
    /// </summary>
    /// <param name="genericType"></param>
    /// <returns></returns>
    internal delegate bool VerifyOptionGenericArgumentCallback(Type genericType);
}
