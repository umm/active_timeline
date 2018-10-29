using System;
using ActiveTimeline.Structure;
using UniRx;
using UnityEngine;

namespace ActiveTimeline.Playables
{

    [Serializable]
    public class JumpBehaviour : ConditionalActiveBehaviourBase
    {
        [SerializeField] private TransitionablePredicate predicateToJump;
        private ITransitionablePredicate PredicateToJump => predicateToJump;

        protected override void Initialize()
        {
            DoBehaviourStartAsObservable()
                .SelectMany(_ => ResolvePredicateObservable(PredicateToJump))
                .Subscribe(_ => JumpTo(PredicateToJump));
        }
    }
}