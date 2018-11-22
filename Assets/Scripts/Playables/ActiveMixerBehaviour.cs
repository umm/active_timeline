using System.Collections.Generic;
using System.Linq;
using ActiveTimeline.Structure;
using UniRx;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ActiveTimeline.Playables
{
    public class ActiveMixerBehaviour : PlayableBehaviour
    {
        public IDictionary<string, Marker> MarkerMap { get; } = new Dictionary<string, Marker>();

        public IReactiveProperty<double> TimeProperty { get; } = new ReactiveProperty<double>();

        public IList<ActiveBehaviourBase> ActiveBehaviourList { get; } = new List<ActiveBehaviourBase>();

        // 各 ActiveMixerBehaviour に持つのはどうかと思うが、妙案浮かばず…。
        // PlayableDirector のインスタンス単位に配下の ActiveTrack をキャッシュしたいのだが、
        // Singleton にするのも嫌なので無理矢理参照を持たせることにした
        public IEnumerable<ActiveTrack> ActiveTracks { get; private set; }

        public float FrameRate { get; set; } = 60.0f;

        private Playable RootPlayable { get; set; }

        private PlayableDirector PlayableDirector { get; set; }

        public override void OnGraphStart(Playable playable)
        {
            // コイツは UnityEngine.Timeline.TimelinePlayable であるコトを期待している
            RootPlayable = playable.GetGraph().GetRootPlayable(0);
            PlayableDirector = (PlayableDirector) playable.GetGraph().GetResolver();
            ActiveTracks = ((TimelineAsset) PlayableDirector.playableAsset).GetOutputTracks().OfType<ActiveTrack>();
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
