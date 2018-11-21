using System;
using System.Collections.Generic;
using System.Linq;
using ActiveTimeline.Enumerate;
using UniRx;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    [AddComponentMenu("ActiveTimeline/Condition/Logical/And", (int)ConditionType.And)]
    public class AndCondition : ConditionBase
    {
        [SerializeField] private List<ConditionBase> conditionList;
        private IEnumerable<ICondition> ConditionList => conditionList;

        public override IObservable<Unit> OnFulfilledAsObservable()
        {
            return ConditionList.Select(x => x.OnFulfilledAsObservable()).Zip().AsUnitObservable();
        }
    }
}
