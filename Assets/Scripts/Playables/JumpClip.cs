using UnityEngine.Timeline;

namespace ActiveTimeline.Playables
{
    public class JumpClip : ActiveClipBase<JumpBehaviour>
    {
        public override ClipCaps clipCaps => ClipCaps.Looping | ClipCaps.Blending | ClipCaps.Extrapolation | ClipCaps.ClipIn;
    }
}