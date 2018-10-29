using UnityEngine.Timeline;

namespace ActiveTimeline.Playables
{
    public class LoopClip : ActiveClipBase<LoopBehaviour>
    {
        public override ClipCaps clipCaps => ClipCaps.Looping | ClipCaps.Blending | ClipCaps.Extrapolation | ClipCaps.ClipIn;
    }
}