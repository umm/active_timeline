using System;
using ActiveTimeline.Enumerate;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    [AddComponentMenu("ActiveTimeline/PointerEvent/Enter", (int)ConditionType.PointerEnter)]
    public class PointerEnterCondition : PointerEventCondition
    {
        public override IObservable<Unit> OnFulfilledAsObservable()
        {
            return UIBehaviour.OnPointerEnterAsObservable().AsUnitObservable();
        }
    }
}