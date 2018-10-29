using System;
using UniRx;

namespace ActiveTimeline
{
    public interface ICondition
    {
        IObservable<Unit> OnFulfilledAsObservable();
    }
}