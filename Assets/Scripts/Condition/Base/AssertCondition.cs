using System;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    [PublicAPI]
    public abstract class AssertCondition<T> : Condition
        where T : IEquatable<T>
    {
        private IReactiveProperty<T> ReactiveProperty { get; } = new ReactiveProperty<T>();
        [SerializeField] private T expected;
        [SerializeField] private T actual;

        protected T Expected
        {
            get { return expected; }
            set { expected = value; }
        }

        protected T Actual
        {
            get { return ReactiveProperty.Value; }
            set { ReactiveProperty.Value = value; }
        }

        protected virtual void Awake()
        {
            this.ObserveEveryValueChanged(x => x.actual).Subscribe(x => Actual = x);
        }

        private IObservable<T> OnValueChangedAsObservable()
        {
            return ReactiveProperty;
        }

        public override IObservable<Unit> OnFulfilledAsObservable()
        {
            return OnValueChangedAsObservable().Where(_ => Actual.Equals(Expected)).AsUnitObservable();
        }
    }
}