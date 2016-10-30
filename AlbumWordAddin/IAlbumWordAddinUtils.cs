using System.Runtime.InteropServices;

namespace AlbumWordAddin
{
    [ComVisible(true)]
    public interface IAlbumWordAddinUtils
    {
        void RemoveEmptyPages();
        void SelectShapesOnPage();
        void AlignSelectedImages(Alignment alignment, float forcedValue);
    }
}