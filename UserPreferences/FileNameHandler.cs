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

        public FileNameHandler(UserPreferences.UserPreferences userPrefs)
        {
            var smallFileNameMakerRe = new Regex(@"\.(jpg|jpeg)$", RegexOptions.IgnoreCase);
            _filePattern = RegexFromPatternList(userPrefs.IncludeFiles);
            _excludePattern = string.IsNullOrWhiteSpace(userPrefs.ExcludeFiles) ? null : RegexFromPatternList(userPrefs.ExcludeFiles);
            _smallPattern = new Regex(@"\.small\.((jpeg)|(jpg))$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            _smallFileNameMaker = s => smallFileNameMakerRe.Replace(s, ".small.$1");
            _largeFileNameMaker = s => new Regex(@"(.*)\.small\.(jpg|jpeg)$", RegexOptions.IgnoreCase).Replace(s, "$1.$2");
        }

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

        public bool FileMatch(string fileFullName, bool includeSmalls)
            => (FilePatternIsMatch(fileFullName) || SmallPatternIsMatch(fileFullName))
            && (_excludePattern==null            || !_excludePattern.Match(fileFullName).Success)
            && (includeSmalls                    || !SmallPatternIsMatch(fileFullName));

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