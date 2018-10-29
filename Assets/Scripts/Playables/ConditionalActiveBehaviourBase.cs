using System;
using ActiveTimeline.Structure;
using UniRx;

namespace ActiveTimeline.Playables
{
    [Serializable]
    public abstract class ConditionalActiveBehaviourBase : ActiveBehaviourBase
    {
        private IObservable<Unit> ResolveTimingObservable(IPredicate predicate)
        {
            return
                predicate.CheckEveryFrame && ActiveMixerBehaviour != null
                    ? ActiveMixerBehaviour.TimeProperty.AsUnitObservable().Take(1)
                    : WillBehaviourEndAsObservable().Take(1);
        }

        protected IObservable<Unit> ResolveFulfilledObservable(IPredicate predicate, bool fulfillIfEmpty = true)
        {
            if (predicate == null || predicate.Condition.Resolve(ExposedPropertyTable) == null)
            {
                return fulfillIfEmpty ? Observable.ReturnUnit() : Observable.Empty<Unit>();
            }
            return predicate.Condition.Resolve(ExposedPropertyTable).OnFulfilledAsObservable().Take(1);
        }

        protected IObservable<Unit> ResolvePredicateObservable(IPredicate predicate)
        {
            return Observable
                .Zip(
                    ResolveTimingObservable(predicate).Do(_ => UnityEngine.Debug.Log("ResolveTimingObservable")),
                    ResolveFulfilledObservable(predicate).Do(_ => UnityEngine.Debug.Log("ResolveFulfilledObservable"))
                )
                .AsUnitObservable();
        }

    }
}