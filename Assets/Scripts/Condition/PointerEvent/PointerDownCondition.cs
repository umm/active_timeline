using System;
using ActiveTimeline.Enumerate;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    [AddComponentMenu("ActiveTimeline/PointerEvent/Down", (int)ConditionType.PointerDown)]
    public class PointerDownCondition : PointerEventCondition
    {
        public override IObservable<Unit> OnFulfilledAsObservable()
        {
            return UIBehaviour.OnPointerDownAsObservable().AsUnitObservable();
        }
    }
}