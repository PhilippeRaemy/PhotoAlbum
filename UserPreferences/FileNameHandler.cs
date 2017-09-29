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
        readonly Regex _rightPattern;
        readonly Func<string, string> _smallFileNameMaker;
        readonly Func<string, string> _largeFileNameMaker;
        readonly Func<string, string> _rightFileNameMaker;

        public FileNameHandler(UserPreferences.UserPreferences userPrefs)
        {
            var smallFileNameMakerRe = new Regex(@"\.(jpg|jpeg)$", RegexOptions.IgnoreCase);
            _filePattern = RegexFromPatternList(userPrefs.IncludeFiles);
            _excludePattern = string.IsNullOrWhiteSpace(userPrefs.ExcludeFolders) ? null : RegexFromPatternList(userPrefs.ExcludeFolders);
            _smallPattern = new Regex(@"\.small\.((jpeg)|(jpg))$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            _rightPattern = new Regex(@"\.right\.((jpeg)|(jpg))$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            _smallFileNameMaker = s => smallFileNameMakerRe.Replace(_largeFileNameMaker(s), ".small.$1");
            _rightFileNameMaker = s => smallFileNameMakerRe.Replace(_largeFileNameMaker(s), ".right.$1");
            _largeFileNameMaker = s => new Regex(@"(.*)\.(small|right)\.(jpg|jpeg)$", RegexOptions.IgnoreCase).Replace(s, "$1.$3");
        }

        public FileNameHandler(
            string fileMaskList, 
            string excludeMaskList, 
            string smallPattern, 
            Func<string, string> smallFileNameMaker,
            Func<string, string> rightFileNameMaker,
            Func<string, string> largeFileNameMaker
        )
        {
            _smallFileNameMaker = smallFileNameMaker;
            _rightFileNameMaker = rightFileNameMaker;
            _largeFileNameMaker = largeFileNameMaker;
            _filePattern        = RegexFromPatternList(fileMaskList);
            _excludePattern     = string.IsNullOrWhiteSpace(excludeMaskList) ? null : RegexFromPatternList(excludeMaskList);
            _smallPattern       = new Regex(smallPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public string SmallFileNameMaker(string s)
            => SmallPatternIsMatch(s)
            ? s
            : _smallFileNameMaker?.Invoke(s) ?? s;

        public string RightFileNameMaker(string s)
            => RightPatternIsMatch(s)
            ? s
            : _rightFileNameMaker?.Invoke(s) ?? s;

        public string LargeFileNameMaker(string s)
            => SmallPatternIsMatch(s) | RightPatternIsMatch(s)
            ? _largeFileNameMaker?.Invoke(s) ?? s
            : s;

        public bool FileMatch(string fileFullName, bool includeSmalls)
            => FilePatternIsMatch(fileFullName)
            || includeSmalls 
                && (SmallPatternIsMatch(fileFullName) || RightPatternIsMatch(fileFullName));

        public bool SmallPatternIsMatch(string fileFullName) => _smallPattern.Match(fileFullName).Success;
        public bool RightPatternIsMatch(string fileFullName) => _rightPattern.Match(fileFullName).Success;
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

        public bool FolderExcludeMatch(string folderFromName) 
            => _excludePattern?.Match(folderFromName).Success ?? false;

    }
}