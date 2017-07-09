namespace AlbumWordAddinTests.TestHelpers
{
    using System;
    using AlbumWordAddin;

    internal class Validation
    {
        public Validation(string message, Func<Rectangle[], Rectangle[], bool> test)
        {
            Message = message;
            Test = test;
        }
        public string Message { get; }
        public Func<Rectangle[], Rectangle[], bool> Test { get; }

    }
}