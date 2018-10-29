using System;
using ActiveTimeline.Structure;
using UniRx;
using UnityEngine;

namespace ActiveTimeline.Playables
{
    [Serializable]
    public class LoopBehaviour : ConditionalActiveBehaviourBase
    {
        [SerializeField] private Predicate predicateToBreak;
        private IPredicate PredicateToBreak => predicateToBreak;

        protected override void Initialize()
        {
            DidBehaviourStartAsObservable()
                .SelectMany(_ => ResolvePredicateObservable(PredicateToBreak))
                .TakeUntil(DidBehaviourEndAsObservable())
                .Subscribe(_ => Break());
            WillBehaviourEndAsObservable()
                .Subscribe(_ => Loop());
        }

        private void Loop()
        {
            ActiveMixerBehaviour.SetTime(ActiveMixerBehaviour.MarkerMap[Label].StartTime);
        }

        private void Break()
        {
            // Jump to next frame of last frame of current clip
            ActiveMixerBehaviour.SetTime(ActiveMixerBehaviour.MarkerMap[Label].EndTime + 1 / ActiveMixerBehaviour.FrameRate);
            // Set processing flag false for prevent firing end behaviour
            SetProcessing(false);
        }
    }
}