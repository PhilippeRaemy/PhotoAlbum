namespace AlbumWordAddin
{
    using System;
    using System.Text.RegularExpressions;
    using MoreLinq;

    public class FileNameHandler
    {
        readonly Regex _filePattern;
        readonly Regex _excludePattern;
        readonly Regex _smallPattern;
        readonly Func<string, string> _smallFileNameMaker;
        readonly Func<string, string> _largeFileNameMaker;

        public FileNameHandler(
            string fileMaskList, 
            string excludeMaskList, 
            string smallPattern, 
            Func<string, string> smallFileNameMaker, 
            Func<string, string> largeFileNameMaker
        )
        {
            _smallFileNameMaker = smallFileNameMaker;
            _largeFileNameMaker = largeFileNameMaker;
            _filePattern        = RegexFromPatternList(fileMaskList);
            _excludePattern     = string.IsNullOrWhiteSpace(excludeMaskList) ? null : RegexFromPatternList(excludeMaskList);
            _smallPattern       = new Regex(smallPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public string SmallFileNameMaker(string s)
            => SmallPatternIsMatch(s)
            ? s
            : _smallFileNameMaker?.Invoke(s) ?? s;

        public string LargeFileNameMaker(string s)
            => SmallPatternIsMatch(s)
            ? _largeFileNameMaker?.Invoke(s) ?? s
            : s;

        public bool FileMatch(string fileFullName)
            => (FilePatternIsMatch(fileFullName)
                || SmallPatternIsMatch(fileFullName)
               )
               && 
                (_excludePattern==null 
                || !_excludePattern.Match(fileFullName).Success
               );

        public bool SmallPatternIsMatch(string fileFullName) => _smallPattern.Match(fileFullName).Success;
        public bool FilePatternIsMatch (string fileFullName) => _filePattern .Match(fileFullName).Success;

        Regex RegexFromPatternList(string maskList)
        {
            var pattern = "(" + maskList.Replace(".", "\\.")
                              .Replace("?", ".")
                              .Replace("*", ".*")
                              .Split(';')
                              .ToDelimitedString(")|(")
                          + ")";
            return new Regex(
                pattern,
                RegexOptions.Compiled | RegexOptions.IgnoreCase
            );
        }
    }
}