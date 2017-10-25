namespace FolderWalker
{
    using System;
    using System.IO;
    using System.Linq;

    public static class FolderWalker
    {
        public static DirectoryInfo WalkNextFolder(DirectoryInfo currentDirectory, FolderDirection folderDirection)
        {
            switch (folderDirection)
            {
                case FolderDirection.Forward: return WalkNextFolder(currentDirectory);
                case FolderDirection.Backward: return WalkPreviousFolder(currentDirectory);
                default:
                    throw new ArgumentOutOfRangeException(nameof(folderDirection), folderDirection, null);
            }
        }

        static DirectoryInfo WalkNextFolder(DirectoryInfo currentDirectory, bool ignoreSubFolders = false)
        {
            while (true)
            {
                var comparer = new Func<string, string, bool>((f1, f2) => String.Compare(f1, f2, StringComparison.InvariantCultureIgnoreCase) > 0);
                if (currentDirectory == null) return null;
                if (!currentDirectory.Exists) return null;
                if (!ignoreSubFolders)
                {
                    foreach (var subDirectory in currentDirectory.EnumerateDirectories().OrderBy(d => d.Name))
                    {
                        return subDirectory;
                    }
                }
                if (currentDirectory.Parent == null) return null;
                var directory = currentDirectory;
                foreach (var subDirectory in
                    currentDirectory.Parent.EnumerateDirectories().OrderBy(d => d.Name).Where(d => comparer(d.Name, directory.Name)))
                {
                    return subDirectory;
                }
                currentDirectory = currentDirectory.Parent;
                ignoreSubFolders = true;
            }
        }

        static DirectoryInfo WalkPreviousFolder(DirectoryInfo currentDirectory, bool diveSubFolders = false)
        {

            var comparer = new Func<string, string, bool>((b1, b2) => String.Compare(b1, b2, StringComparison.InvariantCultureIgnoreCase) < 0);
            if (currentDirectory == null) return null;
            if (!currentDirectory.Exists) return null;
            if (currentDirectory.Parent == null) return null;
            var directory = currentDirectory;
            if (diveSubFolders)
            {
                foreach (var subDirectory in currentDirectory.EnumerateDirectories().OrderByDescending(d => d.Name))
                {
                    return WalkPreviousFolder(subDirectory, diveSubFolders: true);
                }
                return currentDirectory;
            }

            foreach (
                var subDirectory in
                currentDirectory.Parent.EnumerateDirectories().OrderByDescending(d => d.Name).Where(d => comparer(d.Name, directory.Name)))
            {
                return WalkPreviousFolder(subDirectory, diveSubFolders: true);
            }

            return currentDirectory.Parent;
        }
    }
}
