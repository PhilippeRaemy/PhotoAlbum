namespace AlbumWordAddinTests.TestHelpers

{
    using AlbumWordAddin;

    public static class PositionerExtensionsForTests
    {
        public static Positioner.Parms WithRowsCols(this Positioner.Parms model, int rows, int cols)
        {
            return new Positioner.Parms { Cols = cols, Rows = rows, HShape = model.HShape, VShape = model.VShape, Padding = model.Padding, Margin = model.Margin };
        }
    }


}