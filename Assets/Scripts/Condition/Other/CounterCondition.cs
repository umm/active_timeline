using System;
using System.Collections.Generic;
using System.Linq;
using ActiveTimeline.Enumerate;
using UniRx;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    [AddComponentMenu("ActiveTimeline/Other/Counter", (int)ConditionType.Counter)]
    public class CounterCondition : Condition
    {
        [SerializeField] private int threshold;
        [SerializeField] private List<Condition> conditionList;
        [SerializeField] private bool resetOnSubscribe;
        private int Threshold => threshold;
        // 外部から条件を与えたい場合は SetCondition 的なメソッドを実装するとヨサソウ
        private IEnumerable<ICondition> ConditionList => conditionList;
        private IReactiveProperty<int> CountProperty { get; } = new IntReactiveProperty(0);
        private bool ResetOnSubscribe => resetOnSubscribe;

        private void Awake()
        {
            SubscribeCondition();
        }

        private void SubscribeCondition()
        {
            ConditionList?
                .Select(x => x.OnFulfilledAsObservable())
                .Merge()
                .Subscribe(_ => CountProperty.Value++);
        }

        public override IObservable<Unit> OnFulfilledAsObservable()
        {
            if (ResetOnSubscribe)
            {
                CountProperty.Value = 0;
            }
            return CountProperty.Where(x => x == Threshold).AsUnitObservable();
        }
    }
}