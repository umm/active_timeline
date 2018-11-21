using System;
using UniRx;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    public abstract class ConditionBase : MonoBehaviour, ICondition
    {
        public abstract IObservable<Unit> OnFulfilledAsObservable();
    }
}
