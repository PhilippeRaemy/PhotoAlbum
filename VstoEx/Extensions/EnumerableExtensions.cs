namespace VstoEx.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExtensions
    {
        public static T[] CheapToArray<T>(this IEnumerable<T> enumerable)
            => enumerable as T[] ?? enumerable.ToArray();

        public static IEnumerable<T> Append<T>(this IEnumerable<T> enumerable, T other)
        {
            foreach (var e in enumerable)
                yield return e;
            yield return other;
        }
    }
}
