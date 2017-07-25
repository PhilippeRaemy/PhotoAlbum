namespace VstoEx.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExtensions
    {
        public static T[] CheapToArray<T>(this IEnumerable<T> enumerable)
            => enumerable as T[] ?? enumerable.ToArray();
    }
}
