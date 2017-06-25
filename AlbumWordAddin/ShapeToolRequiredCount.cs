namespace AlbumWordAddin
{
    using System;

    [Flags]
    enum ShapeToolRequiredCount
    {
        None = 0,
        OneShape = 1,
        TwoShapes = 2,
        ThreeShapes = 4,
        OneOrMore = 255,
        TwoOrMore = 254,
        ThreeOrMore = 252,
    }
}