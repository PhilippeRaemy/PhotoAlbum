namespace AlbumWordAddinTests.TestHelpers
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    internal class Validation<T>
    {
        readonly string _message;
        readonly Func<T[], T[], bool> _test;

        /// <summary>
        /// Wraps a tester function and a test message for an array of objects to be tested
        /// </summary>
        /// <param name="message"></param>
        /// <param name="test">A function accepting an array of expected results, and an array of actual results, and returning a bool</param>
        public Validation(string message, Func<T[], T[], bool> test)
        {
            _message = message;
            _test = test;
        }

        public void Test(T[] expected, T[] actual)
        {
            Assert.IsTrue(_test(expected, actual), _message);
        }
    }
}