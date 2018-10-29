using System;
using ActiveTimeline.Enumerate;
using UniRx;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    [AddComponentMenu("ActiveTimeline/Other/Timer", (int)ConditionType.Timer)]
    public class TimerCondition : Condition
    {
        [SerializeField] private float duration;
        private float Duration => duration;

        private IObservable<Unit> observable;

        public override IObservable<Unit> OnFulfilledAsObservable()
        {
            return
                observable ?? (
                    observable = Observable
                        .Timer(TimeSpan.FromSeconds(Duration))
                        .AsUnitObservable()
                );
        }
    }
}