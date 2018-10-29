using System;
using ActiveTimeline.Enumerate;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    [AddComponentMenu("ActiveTimeline/PointerEvent/EndDrag", (int)ConditionType.EndDrag)]
    public class EndDragCondition : PointerEventCondition
    {
        public override IObservable<Unit> OnFulfilledAsObservable()
        {
            return UIBehaviour.OnEndDragAsObservable().AsUnitObservable();
        }
    }
}