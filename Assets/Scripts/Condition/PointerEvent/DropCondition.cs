using System;
using ActiveTimeline.Enumerate;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    [AddComponentMenu("ActiveTimeline/Condition/PointerEvent/Drop", (int)ConditionType.Drop)]
    public class DropCondition : PointerEventConditionBase
    {
        public override IObservable<Unit> OnFulfilledAsObservable()
        {
            return UIBehaviour.OnDropAsObservable().AsUnitObservable();
        }
    }
}
