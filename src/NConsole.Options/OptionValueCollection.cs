using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NConsole.Options
{
    using static Characters;
    using static String;

    public class OptionValueCollection : IList, IList<string>
    {
        /// <summary>
        /// Gets the Values used within the Collection.
        /// </summary>
        private List<string> Values { get; } = new List<string>();

        /// <summary>
        /// Gets the Context used within the Collection.
        /// </summary>
        private OptionContext Context { get; }

        internal OptionValueCollection(OptionContext context)
        {
            Context = context;
        }

        #region Callbacks

        /// <summary>
        /// Allows for functional Actions or Functions depending on the view
        /// from which we must depend upon <see cref="Values"/>.
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <returns></returns>
        private delegate TTarget CollectionGetTargetCallback<out TTarget>();

        /// <summary>
        /// Allows for a <typeparamref name="TTarget"/> specific <see cref="Action"/>
        /// like callback.
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="target"></param>
        private delegate void CollectionActionCallback<in TTarget>(TTarget target);

        /// <summary>
        /// Allows for a <typeparamref name="TTarget"/> specific <see cref="Func{TResult}"/>
        /// like callback.
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        private delegate TResult CollectionFuncCallback<in TTarget, out TResult>(TTarget target);

        /// <summary>
        /// Invoke this helper method when <see cref="Action"/> like callbacks are required.
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="getTarget"></param>
        /// <param name="targetCallback"></param>
        private static void CollectionAction<TTarget>(CollectionGetTargetCallback<TTarget> getTarget
            , CollectionActionCallback<TTarget> targetCallback)
            => targetCallback(getTarget());

        /// <summary>
        /// Invoke this helper method when <see cref="Func{TResult}"/> like callbacks are
        /// required.
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="getTarget"></param>
        /// <param name="factoryCallback"></param>
        /// <returns></returns>
        private static TResult CollectionFunc<TTarget, TResult>(CollectionGetTargetCallback<TTarget> getTarget
            , CollectionFuncCallback<TTarget, TResult> factoryCallback)
            => factoryCallback(getTarget());

        #endregion // Callbacks

        #region ICollection

        /// <inheritdoc cref="ICollection" />
        public void CopyTo(Array array, int index) => CollectionAction(() => Values, (IList x) => x.CopyTo(array, index));

        // TODO: TBD: is there a reason this needs to implement both IList<string> as well as IList?
        //void ICollection.CopyTo(Array array, int index) => CollectionAction(() => Values, (IList x) => x.CopyTo(array, index));

        /// <inheritdoc cref="ICollection" />
        public bool IsSynchronized => CollectionFunc(() => Values, (IList x) => x.IsSynchronized);

        //bool ICollection.IsSynchronized => CollectionFunc(() => Values, (IList x) => x.IsSynchronized);

        /// <inheritdoc cref="ICollection" />
        public object SyncRoot => CollectionFunc(() => Values, (IList x) => x.SyncRoot);

        #endregion // ICollection

        #region ICollection<T>

        /// <inheritdoc cref="ICollection{T}" />
        public void Add(string item) => CollectionAction(() => Values, (IList<string> x) => x.Add(item));

        /// <inheritdoc cref="ICollection{T}" />
        public void Clear() => CollectionAction(() => Values, (IList<string> x) => x.Clear());

        /// <inheritdoc cref="ICollection{T}" />
        public bool Contains(string item) => CollectionFunc(() => Values, (IList<string> x) => x.Contains(item));

        /// <inheritdoc cref="ICollection{T}" />
        public void CopyTo(string[] array, int arrayIndex) => CollectionAction(() => Values, (IList<string> x) => x.CopyTo(array, arrayIndex));

        /// <inheritdoc cref="ICollection{T}" />
        public bool Remove(string item) => CollectionFunc(() => Values, (IList<string> x) => x.Remove(item));

        /// <inheritdoc cref="ICollection{T}" />
        public int Count => CollectionFunc(() => Values, (IList<string> x) => x.Count);

        /// <inheritdoc cref="ICollection{T}" />
        public bool IsReadOnly { get; } = false;

        #endregion // ICollection<T>

        #region IEnumerable

        /// <inheritdoc cref="IEnumerable" />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion // IEnumerable

        #region IEnumerable<T>

        /// <inheritdoc cref="IEnumerable{T}" />
        public IEnumerator<string> GetEnumerator() => CollectionFunc(() => AssertValidity(Values), (IEnumerable<string> x) => x.GetEnumerator());

        #endregion // IEnumerable<T>

        #region IList

        /// <inheritdoc cref="IList" />
        public int Add(object value) => CollectionFunc(() => Values, (IList x) => x.Add(value));

        /// <inheritdoc cref="IList" />
        public bool Contains(object value) => CollectionFunc(() => Values, (IList x) => x.Contains(value));

        /// <inheritdoc cref="IList" />
        public int IndexOf(object value) => CollectionFunc(() => Values, (IList x) => x.IndexOf(value));

        /// <inheritdoc cref="IList" />
        public void Insert(int index, object value) => CollectionAction(() => Values, (IList x) => x.Insert(index, value));

        /// <inheritdoc cref="IList" />
        public void Remove(object value) => CollectionAction(() => Values, (IList x) => x.Remove(value));

        /// <inheritdoc cref="IList" />
        void IList.RemoveAt(int index) => CollectionAction(() => Values, (IList x) => x.RemoveAt(index));

        /// <inheritdoc cref="IList" />
        public bool IsFixedSize { get; } = false;

        /// <inheritdoc cref="IList" />
        object IList.this[int index]
        {
            get => CollectionFunc(() => Values, (IList x) => x[index]);
            set => CollectionAction(() => Values, (IList x) => x[index] = value);
        }

        #endregion // IList

        #region IList<T>

        /// <inheritdoc cref="IList{T}" />
        public int IndexOf(string item) => CollectionFunc(() => Values, (IList<string> x) => x.IndexOf(item));

        /// <inheritdoc cref="IList{T}" />
        public void Insert(int index, string item) => CollectionAction(() => Values, (IList<string> x) => x.Insert(index, item));

        /// <inheritdoc cref="IList{T}" />
        public void RemoveAt(int index) => CollectionAction(() => Values, (IList<string> x) => x.RemoveAt(index));

        // TODO: TBD: wouldn't this be a valid thing to "assert", including prior to returning the enumerator?
        /// <summary>
        /// Asserts that the Collection is Valid prior to referencing the indexer.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="index"></param>
        private List<string> AssertValidity(List<string> values, int? index = null)
        {
            var option = Context.Option;

            if (option == null)
            {
                throw new InvalidOperationException(
                    $"{Join($"{Dot}", $"{nameof(OptionContext)}", $"{nameof(OptionContext.Option)}")} is null."
                );
            }

            // ReSharper disable once InvertIf
            if (index.HasValue)
            {
                if (index >= option.MaximumValueCount)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                if (option.ValueType == OptionValueType.Required && index >= values.Count)
                {
                    throw new OptionException(Format(
                            Context.OptionSet.MessageLocalizer("Missing required value for option `{0}'.")
                            , Context.OptionName)
                        , Context.OptionName);

                }
            }

            return values;
        }

        /// <inheritdoc cref="IList{T}" />
        public string this[int index]
        {
            // ReSharper disable once ImplicitlyCapturedClosure
            get => CollectionFunc(() => AssertValidity(Values, index)
                , (IList<string> x) => index >= x.Count ? null : x[index]);
            set => CollectionAction(() => Values, (IList<string> x) => x[index] = value);
        }

        #endregion // IList<T>

        // TODO: TBD: do we even need ToList or ToArray here?
        public List<string> ToList() => CollectionFunc(() => Values, (IList<string> x) => new List<string>(x));

        public string[] ToArray() => CollectionFunc(() => Values, (IList<string> x) => x.ToArray());

        /// <inheritdoc />
        public override string ToString() => Join($"{Comma} ", ToArray());
    }
}
