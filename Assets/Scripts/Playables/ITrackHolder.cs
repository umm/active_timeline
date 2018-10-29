using UnityEngine.Timeline;

namespace ActiveTimeline.Playables
{
    public interface ITrackHolder
    {
        TrackAsset Track { get; set; }
    }
}