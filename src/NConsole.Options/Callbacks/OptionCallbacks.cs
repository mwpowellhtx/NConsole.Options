namespace NConsole.Options
{
    /// <summary>
    /// Represents a default Option Callback. This is useful for cases of flags.
    /// </summary>
    public delegate void OptionCallback();

    /// <summary>
    /// Represents an <see cref="Option"/> Callback given a single <paramref name="target"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="target"></param>
    public delegate void OptionCallback<in T>(T target);

    /// <summary>
    /// Represents an <see cref="Option"/> Callback given a <paramref name="key"/>,
    /// <paramref name="value"/> pair oriented invocation.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public delegate void OptionCallback<in TKey, in TValue>(TKey key, TValue value);
}
