using System.Collections.Generic;
using ActiveTimeline.Structure;
using UniRx;
using UnityEngine.Playables;

namespace ActiveTimeline.Playables
{
    public class ActiveMixerBehaviour : PlayableBehaviour
    {
        public Dictionary<string, Marker> MarkerMap { get; } = new Dictionary<string, Marker>();

        public IReactiveProperty<double> TimeProperty { get; } = new ReactiveProperty<double>();

        public float FrameRate { get; set; } = 60.0f;

        private Playable RootPlayable { get; set; }

        private PlayableDirector PlayableDirector { get; set; }

        public override void OnGraphStart(Playable playable)
        {
            // コイツは UnityEngine.Timeline.TimelinePlayable であるコトを期待している
            RootPlayable = playable.GetGraph().GetRootPlayable(0);
            PlayableDirector = playable.GetGraph().GetResolver() as PlayableDirector;
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            TimeProperty.Value = RootPlayable.GetTime();
        }

        public bool IsContainTime(string label)
        {
            return IsContainTime(label, RootPlayable.GetTime());
        }

        public bool IsContainTime(string label, double time)
        {
            return MarkerMap[label].StartTime <= time && time <= MarkerMap[label].EndTime;
        }

        public bool IsProcessing(string label)
        {
            return MarkerMap[label].Processing;
        }

        public void SetProcessing(string label, bool processing)
        {
            var marker = MarkerMap[label];
            marker.Processing = processing;
            MarkerMap[label] = marker;
        }

        public double GetDuration()
        {
            return RootPlayable.GetDuration();
        }

        public void SetTime(double time)
        {
            if (!RootPlayable.IsValid())
            {
                return;
            }
            RootPlayable.SetTime(time);
        }

        public void Stop()
        {
            PlayableDirector.Stop();
        }

        public void Pause()
        {
            PlayableDirector.Pause();
        }

        public void Resume()
        {
            PlayableDirector.Resume();
        }
    }
}