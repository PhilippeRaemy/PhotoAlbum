namespace PicturesSorter
{
    using System.Collections.Generic;
    using System.Drawing;

    public enum Side { Left, Right}
    public enum Shelve { Shelve, Unshelve, None }

    internal static class OperationStack
    {
        static readonly Stack<Operation> Operations = new Stack<Operation>();


        internal class Operation
        {
            public Side Side;
            public ImageHost ImageHost;
            public RotateFlipType RotateFlipType;
            public Shelve Shelve;
        }

        public static void Push(Side side, ImageHost imageHost, RotateFlipType rotateFlipType = RotateFlipType.RotateNoneFlipNone, Shelve shelve = Shelve.None)
        {
            Operations.Push(new Operation { Side = side, ImageHost = imageHost, RotateFlipType = rotateFlipType, Shelve = shelve});
        }

        public static Operation Pop()
        {
            return Operations.Pop();
        }
    }
}