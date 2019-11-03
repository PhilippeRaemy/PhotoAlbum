namespace AlbumWordAddinTests.TestHelpers

{
    using AlbumWordAddin.Positioning;

    public static class PositionerExtensionsForTests
    {
        public static PositionerParms WithRowsCols(this PositionerParms model, int rows, int cols)
        {
            return new PositionerParms { Cols = cols, Rows = rows, HShape = model.HShape, VShape = model.VShape, Spacing = model.Spacing, Margin = model.Margin };
        }
    }


}