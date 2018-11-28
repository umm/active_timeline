using System;
using ActiveTimeline.Enumerate;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    [AddComponentMenu("ActiveTimeline/Condition/PointerEvent/Click", (int)ConditionType.PointerClick)]
    public class PointerClickCondition : PointerEventConditionBase
    {
        public override IObservable<Unit> OnFulfilledAsObservable()
        {
            return UIBehaviour.OnPointerClickAsObservable().AsUnitObservable();
        }
    }
}
