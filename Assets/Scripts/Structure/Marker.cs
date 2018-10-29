namespace ActiveTimeline.Structure
{
    public struct Marker
    {
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public bool Processing { get; set; }
    }
}