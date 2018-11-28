using System;
using System.Collections.Generic;
using System.Linq;
using ActiveTimeline.Enumerate;
using ActiveTimeline.Structure;
using UniRx;
using UnityEngine;

namespace ActiveTimeline.Playables
{
    [Serializable]
    public class GoToBehaviour : ActiveBehaviourBase
    {
        [SerializeField] private DefaultBehaviour defaultBehaviour;
        [SerializeField] private List<TransitionablePredicate> predicateList = new List<TransitionablePredicate>();

        private DefaultBehaviour DefaultBehaviour => defaultBehaviour;
        private IEnumerable<ITransitionablePredicate> PredicateList => predicateList.Cast<ITransitionablePredicate>();

        protected override void Initialize()
        {
            var observable = DoBehaviourStartAsObservable()
                .SelectMany(
                    _ => PredicateList
                        .Select(x => ResolvePredicateObservable(x).Select(__ => x))
                        .Merge()
                        .Take(1)
                )
                .Share();
            observable.Subscribe(JumpTo);
            DoBehaviourEndAsObservable()
                .TakeUntil(observable)
                .Subscribe(_ => RunDefaultBehaviour());
        }

        private IObservable<Unit> ResolveTimingObservable(IPredicate predicate)
        {
            return
                predicate.CheckEveryFrame && ActiveMixerBehaviour != null
                    ? ActiveMixerBehaviour.TimeProperty.AsUnitObservable().Take(1)
                    : WillBehaviourEndAsObservable().Take(1);
        }

        private IObservable<Unit> ResolveFulfilledObservable(IPredicate predicate, bool fulfillIfEmpty = true)
        {
            if (predicate == null || predicate.Condition.Resolve(ExposedPropertyTable) == null)
            {
                return fulfillIfEmpty ? Observable.ReturnUnit() : Observable.Empty<Unit>();
            }

            return predicate.Condition.Resolve(ExposedPropertyTable).OnFulfilledAsObservable().Take(1);
        }

        private IObservable<Unit> ResolvePredicateObservable(IPredicate predicate)
        {
            return Observable
                .Zip(
                    ResolveTimingObservable(predicate),
                    ResolveFulfilledObservable(predicate)
                )
                .AsUnitObservable();
        }

        private void RunDefaultBehaviour()
        {
            switch (DefaultBehaviour)
            {
                case DefaultBehaviour.Through:
                    // Do nothing.
                    break;
                case DefaultBehaviour.Loop:
                    JumpTo(
                        new TransitionablePredicate
                        {
                            TargetType = TargetType.Clip,
                            Label = Label,
                        }
                    );
                    break;
                case DefaultBehaviour.Wait:
                    ActiveMixerBehaviour.Pause();
                    PredicateList
                        .Select(x => ResolvePredicateObservable(x).Select(__ => x))
                        .Merge()
                        .Take(1)
                        .Subscribe(_ => ActiveMixerBehaviour.Resume());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
