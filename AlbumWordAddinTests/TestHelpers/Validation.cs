using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestsAlbumWordAddin.TestHelpers
{
    internal class Validation<T>
    {
        readonly string _message;
        readonly Func<IEnumerable<T>, IEnumerable<T>, bool> _test;

        /// <summary>
        /// Wraps a tester function and a test message for an array of objects to be tested
        /// </summary>
        /// <param name="message"></param>
        /// <param name="test">A function accepting an enumeration of expected results, and an enumeration of actual results, and returning a bool</param>
        public Validation(string message, Func<IEnumerable<T>, IEnumerable<T>, bool> test)
        {
            _message = message;
            _test = test;
        }

        public void Test(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            Assert.IsTrue(_test(expected, actual), _message);
        }
    }
}