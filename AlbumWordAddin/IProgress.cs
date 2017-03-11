﻿namespace AlbumWordAddin
{
    public interface IProgress
    {
        void InitProgress(int max, string caption);
        void Progress(string text);
        void CloseProgress();
    }
}