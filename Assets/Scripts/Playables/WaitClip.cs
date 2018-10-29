using UnityEngine.Timeline;

namespace ActiveTimeline.Playables
{
    public class WaitClip : ActiveClipBase<WaitBehaviour>
    {
        public override ClipCaps clipCaps => ClipCaps.Looping | ClipCaps.Blending | ClipCaps.Extrapolation | ClipCaps.ClipIn;
    }
}