using System;
using System.Collections.Generic;
using System.Linq;
using ActiveTimeline.Structure;
using UniRx;
using UnityEngine;

namespace ActiveTimeline.Playables
{
    [Serializable]
    public class SwitchBehaviour : ConditionalActiveBehaviourBase
    {
        [SerializeField]
        private List<TransitionablePredicate> predicateList;

        private IEnumerable<ITransitionablePredicate> PredicateList => predicateList.Cast<ITransitionablePredicate>();

        protected override void Initialize()
        {
            DoBehaviourStartAsObservable()
                .SelectMany(
                    _ => PredicateList
                        .Select(x => ResolvePredicateObservable(x).Select(__ => x))
                        .Merge()
                        .Take(1)
                )
                .Subscribe(JumpTo);
        }
    }
}