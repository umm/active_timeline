using System;
using ActiveTimeline.Enumerate;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    [AddComponentMenu("ActiveTimeline/PointerEvent/BeginDrag", (int)ConditionType.BeginDrag)]
    public class BeginDragCondition : PointerEventCondition
    {
        public override IObservable<Unit> OnFulfilledAsObservable()
        {
            return UIBehaviour.OnBeginDragAsObservable().AsUnitObservable();
        }
    }
}