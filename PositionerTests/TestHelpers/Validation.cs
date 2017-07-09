namespace AlbumWordAddinTests.TestHelpers
{
    using System;
    using AlbumWordAddin;

    internal class Validation<T>
    {
        public Validation(string message, Func<T[], T[], bool> test)
        {
            Message = message;
            Test = test;
        }
        public string Message { get; }
        public Func<T[], T[], bool> Test { get; }

    }
}