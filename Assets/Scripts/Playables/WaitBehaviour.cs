using System;
using ActiveTimeline.Structure;
using UniRx;
using UnityEngine;

namespace ActiveTimeline.Playables
{
    [Serializable]
    public class WaitBehaviour : ConditionalActiveBehaviourBase
    {
        [SerializeField] private Predicate predicateToResume;
        private IPredicate PredicateToResume => predicateToResume;

        protected override void Initialize()
        {
            DoBehaviourEndAsObservable().Subscribe(_ => Pause());
        }

        private void Pause()
        {
            ActiveMixerBehaviour.Pause();
            ResolveFulfilledObservable(PredicateToResume).Subscribe(_ => Resume());
        }

        private void Resume()
        {
            ActiveMixerBehaviour.Resume();
        }
    }
}