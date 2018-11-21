using System;
using ActiveTimeline.Enumerate;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    [AddComponentMenu("ActiveTimeline/Condition/PointerEvent/EndDrag", (int)ConditionType.EndDrag)]
    public class EndDragCondition : PointerEventConditionBase
    {
        public override IObservable<Unit> OnFulfilledAsObservable()
        {
            return UIBehaviour.OnEndDragAsObservable().AsUnitObservable();
        }
    }
}
