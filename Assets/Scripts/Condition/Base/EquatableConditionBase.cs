using System;
using ActiveTimeline.Value;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    [PublicAPI]
    public abstract class EquatableConditionBase<T, TConcrete> : ConditionBase
        where T : IEquatable<T>
        where TConcrete : ValueBase<T>
    {
        [SerializeField] protected TConcrete expected;
        [SerializeField] protected TConcrete actual;

        protected T Expected
        {
            get { return expected.Value; }
            set { expected.Value = value; }
        }

        protected T Actual
        {
            get { return actual.Value; }
            set { actual.Value = value; }
        }

        public override IObservable<Unit> OnFulfilledAsObservable()
        {
            return Observable
                .ZipLatest(expected.ReactiveProperty, actual.ReactiveProperty)
                .Where(list => Equals(list[0], list[1]))
                .AsUnitObservable();
        }
    }
}
