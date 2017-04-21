namespace PicturesSorter
{
    using System.Collections.Generic;
    using System.Linq;
    using MoreLinq;

    internal static class GenericExtentions {
        static LinkedListNode<T> SafeNext<T>(this LinkedListNode<T> lln) => lln?.Next ?? lln?.List?.First ;
        static LinkedListNode<T> SafePrev<T>(this LinkedListNode<T> lln) => lln?.Previous ?? lln?.List?.Last;
        public static LinkedListNode<T> SafeStep<T>(this LinkedListNode<T> lln, int step) 
            => step < 0 ? lln?.SafePrev()
                : step > 0 ? lln?.SafeNext()
                    : lln;

        public static int IndexOf<T>(this LinkedList<T> ll, T lln) where T:class
        {
            var theOne = ll.Index().FirstOrDefault(n => n.Value.Equals(lln));
            return theOne.Value==null ? -1 : theOne.Key;
        }
    }
}