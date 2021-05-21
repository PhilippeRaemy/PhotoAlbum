namespace VstoEx.Geometry
{
    public class Segment
    {
        float Start { get; }
        float End { get; }
        float Size => End - Start;

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

        bool Contains(Segment other)
            => Start <= other.Start && End >= other.End;

        public bool Contains(float value)
            => Start <= value && value <= End;

        public float DistanceTo(Segment other)
            => other.Start >= End   ? other.Start - End // Righter disjoint
             : Start >= other.End   ? Start - other.End // Lefter disjoint
             : Contains(other)      ? other.Size - Size // Containing
             : other.Contains(this) ? Size - other.Size // Contained
             : Start <= other.Start ? other.Start - End // Righter overlaping
             : other.Start <= Start ? Start - other.End // Lefter overlaping
             : float.NaN;

        public bool Overlaps(Segment other)
            => Contains(other)
               || other.Contains(this)
               || Start <= other.Start && other.Start <= End
               || other.Start <= Start && Start <= other.End;

        public bool OverlapsAbsolute(Segment other)
            => Contains(other)
               || other.Contains(this)
               || Start < other.Start && other.Start < End
               || other.Start < Start && Start < other.End;

        public override string ToString() => $"[{Start}, {End}]";
    }
}