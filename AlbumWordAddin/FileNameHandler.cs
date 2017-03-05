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

        public FileNameHandler(string fileMaskList, string excludeMaskList, string smallPattern, Func<string, string> smallFileNameMaker)
        {
            _smallFileNameMaker = smallFileNameMaker;
            _filePattern        = RegexFromPatternList(fileMaskList);
            _excludePattern     = RegexFromPatternList(excludeMaskList);
            _smallPattern       = new Regex(smallPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public string SmallFileNameMaker(string s)
            => _smallFileNameMaker?.Invoke(s) ?? s;

        public bool FileMatch(string fileFullName)
            => (FilePatternIsMatch(fileFullName)
                || SmallPatternIsMatch(fileFullName)
               )
               && !_excludePattern.Match(fileFullName).Success;

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