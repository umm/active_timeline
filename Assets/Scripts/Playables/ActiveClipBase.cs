using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ActiveTimeline.Playables
{
    public abstract class ActiveClipBase<T> : PlayableAsset, ITimelineClipAsset, ILabelProvider, ITrackHolder
        where T : ActiveBehaviourBase, new()
    {
        [SerializeField] private string label = Regex.Replace(typeof(T).Name, "^(.*)Behaviour$", "$1");
        [SerializeField] private T template = new T();

        public string Label => label;
        private T Template => template;
        TrackAsset ITrackHolder.Track { get; set; }

        public abstract ClipCaps clipCaps { get; }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<T>.Create(graph, Template);
            var behaviour = playable.GetBehaviour();
            behaviour.Label = Label;
            return playable;
        }
    }
}