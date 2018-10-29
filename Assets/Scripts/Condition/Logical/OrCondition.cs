using System;
using ActiveTimeline.Enumerate;
using UniRx;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    [AddComponentMenu("ActiveTimeline/Logical/Or", (int)ConditionType.Or)]
    public class OrCondition : Condition
    {
        [SerializeField] private Condition left;
        [SerializeField] private Condition right;
        private ICondition Left => left;
        private ICondition Right => right;

        public override IObservable<Unit> OnFulfilledAsObservable()
        {
            // ReSharper disable once InvokeAsExtensionMethod
            return Observable.Merge(Left.OnFulfilledAsObservable(), Right.OnFulfilledAsObservable());
        }
    }
}