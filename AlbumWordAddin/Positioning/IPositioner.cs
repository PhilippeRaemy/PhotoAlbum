namespace AlbumWordAddin.Positioning
{
    using System.Collections.Generic;
    using VstoEx.Geometry;

    public interface IPositioner
    {
        IEnumerable<Rectangle> DoPosition(PositionerParms parms, Rectangle clientArea, IEnumerable<Rectangle> rectangles);
    }
}