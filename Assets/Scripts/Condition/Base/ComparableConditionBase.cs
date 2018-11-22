using System;
using System.Collections.Generic;
using ActiveTimeline.Enumerate;
using ActiveTimeline.Value;
using UniRx;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    public abstract class ComparableConditionBase<T, TConcrete> : EquatableConditionBase<T, TConcrete>
        where T : IEquatable<T>, IComparable<T>
        where TConcrete : ValueBase<T>
    {
        [SerializeField] private Operand operand;
        private Operand Operand => operand;

        private static IDictionary<Operand, Func<T, T, bool>> OperandDelegateMap { get; } = new Dictionary<Operand, Func<T, T, bool>>
        {
            {Operand.Equal, (arg1, arg2) => Equals(arg1, arg2)},
            {Operand.GreaterThan, (arg1, arg2) => arg1.CompareTo(arg2) < 0},
            {Operand.GreaterThanOrEqual, (arg1, arg2) => arg1.CompareTo(arg2) <= 0},
            {Operand.LessThan, (arg1, arg2) => arg1.CompareTo(arg2) > 0},
            {Operand.LessThanOrEqual, (arg1, arg2) => arg1.CompareTo(arg2) >= 0},
        };

        public override IObservable<Unit> OnFulfilledAsObservable()
        {
            return Observable
                .ZipLatest(expected.ReactiveProperty, actual.ReactiveProperty)
                .Where(list => OperandDelegateMap[Operand](list[0], list[1]))
                .AsUnitObservable();
        }

    }
}
