namespace VstoEx.Geometry
{
    using System;
    using Microsoft.Office.Interop.Word;

    public class Segment
    {
        public float Start { get; }
        public float End { get; }
        public float Size => End - Start;

        public Segment(float start, float end)
        {
            if (end > start + float.Epsilon)
            {
                Start = start;
                End = end;
            }
            else
            {
                Start = end;
                End = start;
            }
        }

        public float DistanceTo(Segment other)
            => other.Start >= End                       ? other.Start - End // Righter disjoint
             : Start >= other.End                       ? Start - other.End // Lefter disjoint
             : Start <= other.Start && End >= other.End ? other.Size - Size // Containing
             : other.Start <= Start && other.End >= End ? Size - other.Size // Contained
             : Start <= other.Start                     ? - (End - other.Start) // Righter overlaping
             : other.Start <= Start                     ? - (other.End - Start) // Lefter overlaping
             : float.PositiveInfinity;

        public bool Overlaps(Segment other)
            => DistanceTo(other) < 0;

        public override string ToString() => $"[{Start}, {End}]";
    }
}