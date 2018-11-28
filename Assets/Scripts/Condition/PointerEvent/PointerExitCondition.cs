using System;
using ActiveTimeline.Enumerate;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    [AddComponentMenu("ActiveTimeline/Condition/PointerEvent/Exit", (int)ConditionType.PointerExit)]
    public class PointerExitCondition : PointerEventConditionBase
    {
        public override IObservable<Unit> OnFulfilledAsObservable()
        {
            return UIBehaviour.OnPointerExitAsObservable().AsUnitObservable();
        }
    }
}
