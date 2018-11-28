using System.Collections.Generic;
using ActiveTimeline.Structure;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ActiveTimeline.Playables
{
    [TrackClipType(typeof(LabelClip))]
    [TrackClipType(typeof(GoToClip))]
    public class ActiveTrack : TrackAsset
    {
        public IDictionary<string, Marker> MarkerMap { get; private set; }

        public ActiveMixerBehaviour ActiveMixerBehaviour { get; private set; }

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

            ActiveMixerBehaviour = behaviour;

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
