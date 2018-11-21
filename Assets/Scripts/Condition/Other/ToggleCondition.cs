using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    public class ToggleCondition : ConditionBase
    {
        [SerializeField] private List<ConditionBase> conditionList;
        [SerializeField] private bool initialValue;
        [SerializeField] private bool resetOnSubscribe;
        private IEnumerable<ICondition> ConditionList => conditionList;
        private bool InitialValue => initialValue;
        private IReactiveProperty<bool> ToggleProperty { get; } = new BoolReactiveProperty();
        private bool ResetOnSubscribe => resetOnSubscribe;

        private void Awake()
        {
            ToggleProperty.Value = InitialValue;
            SubscribeCondition();
        }

        private void SubscribeCondition()
        {
            ConditionList?
                .Select(x => x.OnFulfilledAsObservable())
                .Merge()
                .Subscribe(_ => ToggleProperty.Value = !ToggleProperty.Value);
        }

        public override IObservable<Unit> OnFulfilledAsObservable()
        {
            if (ResetOnSubscribe)
            {
                ToggleProperty.Value = InitialValue;
            }
            return ToggleProperty.Where(x => x).AsUnitObservable();
        }
    }
}
