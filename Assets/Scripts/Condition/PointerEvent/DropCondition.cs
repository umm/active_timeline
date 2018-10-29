using System;
using ActiveTimeline.Enumerate;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    [AddComponentMenu("ActiveTimeline/PointerEvent/Drop", (int)ConditionType.Drop)]
    public class DropCondition : PointerEventCondition
    {
        public override IObservable<Unit> OnFulfilledAsObservable()
        {
            return UIBehaviour.OnDropAsObservable().AsUnitObservable();
        }
    }
}