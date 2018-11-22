using System.Collections.Generic;
using ActiveTimeline.Structure;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ActiveTimeline.Playables
{
    [TrackClipType(typeof(LabelClip))]
    [TrackClipType(typeof(JumpClip))]
    [TrackClipType(typeof(SwitchClip))]
    [TrackClipType(typeof(LoopClip))]
    [TrackClipType(typeof(WaitClip))]
    [TrackClipType(typeof(EventClip))]
    public class ActiveTrack : TrackAsset
    {
        public Dictionary<string, Marker> MarkerMap { get; private set; }

        // CreateTrackMixer は Hierarchy 上の PlayableDirector を選択しているときしか発火しないっぽい
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            var playable = ScriptPlayable<ActiveMixerBehaviour>.Create(graph, inputCount);
            var behaviour = playable.GetBehaviour();
            behaviour.FrameRate = timelineAsset.editorSettings.fps;
            foreach (var clip in GetClips())
            {
                var trackHolder = clip.asset as ITrackHolder;
                if (trackHolder != null)
                {
                    trackHolder.Track = this;
                }

                var label = ResolveLabel(clip);
                clip.displayName = label;

                if (behaviour.MarkerMap.ContainsKey(label))
                {
                    Debug.LogWarning($"Marker name `{label}' already contains in `{name}' track. Please check all clips.");
                    continue;
                }

                behaviour.MarkerMap[label] = new Marker
                {
                    StartTime = clip.start,
                    EndTime = clip.end,
                    Processing = false,
                };
            }

            MarkerMap = behaviour.MarkerMap;

            return playable;
        }

        private static string ResolveLabel(TimelineClip clip)
        {
            var labelProvider = clip.asset as ILabelProvider;
            if (labelProvider == null)
            {
                return string.Empty;
            }

            return string.IsNullOrEmpty(labelProvider.Label) ? string.Empty : labelProvider.Label;
        }
    }
}
