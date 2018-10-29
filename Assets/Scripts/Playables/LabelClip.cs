using UnityEngine.Timeline;

namespace ActiveTimeline.Playables
{
    public class LabelClip : ActiveClipBase<LabelBehaviour>
    {
        public override ClipCaps clipCaps => ClipCaps.Looping | ClipCaps.Blending | ClipCaps.Extrapolation | ClipCaps.ClipIn;
    }
}