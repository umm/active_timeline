using UnityEngine.Timeline;

namespace ActiveTimeline.Playables
{
    public class SwitchClip : ActiveClipBase<SwitchBehaviour>
    {
        public override ClipCaps clipCaps => ClipCaps.Looping | ClipCaps.Blending | ClipCaps.Extrapolation | ClipCaps.ClipIn;
    }
}