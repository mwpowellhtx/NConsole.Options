namespace NConsole.Options
{
    public delegate void OptionCallback<in T>(T target);

    public delegate void OptionCallback<in TKey, in TValue>(TKey key, TValue value);
}
