using UnityEngine.Timeline;

namespace ActiveTimeline.Playables
{
    public class EventClip : ActiveClipBase<EventBehaviour>
    {
        public override ClipCaps clipCaps => ClipCaps.Looping | ClipCaps.Blending | ClipCaps.Extrapolation | ClipCaps.ClipIn;
    }
}
