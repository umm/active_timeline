using System;
using ActiveTimeline.Enumerate;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    [AddComponentMenu("ActiveTimeline/Condition/PointerEvent/Up", (int)ConditionType.PointerUp)]
    public class PointerUpCondition : PointerEventConditionBase
    {
        public override IObservable<Unit> OnFulfilledAsObservable()
        {
            return UIBehaviour.OnPointerUpAsObservable().AsUnitObservable();
        }
    }
}
