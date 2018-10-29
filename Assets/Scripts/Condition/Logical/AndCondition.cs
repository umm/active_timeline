using System;
using ActiveTimeline.Enumerate;
using UniRx;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    [AddComponentMenu("ActiveTimeline/Logical/And", (int)ConditionType.And)]
    public class AndCondition : Condition
    {
        [SerializeField] private Condition left;
        [SerializeField] private Condition right;
        private ICondition Left => left;
        private ICondition Right => right;

        public override IObservable<Unit> OnFulfilledAsObservable()
        {
            return Observable.Zip(Left.OnFulfilledAsObservable(), Right.OnFulfilledAsObservable()).AsUnitObservable();
        }
    }
}