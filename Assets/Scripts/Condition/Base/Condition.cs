using System;
using UniRx;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    public abstract class Condition : MonoBehaviour, ICondition
    {
        public abstract IObservable<Unit> OnFulfilledAsObservable();
    }
}