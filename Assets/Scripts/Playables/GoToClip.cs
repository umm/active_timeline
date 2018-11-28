using UnityEngine.Timeline;

namespace ActiveTimeline.Playables
{
    public class GoToClip : ActiveClipBase<GoToBehaviour>
    {
        public override ClipCaps clipCaps => ClipCaps.Looping | ClipCaps.Blending | ClipCaps.Extrapolation | ClipCaps.ClipIn;
    }
}
