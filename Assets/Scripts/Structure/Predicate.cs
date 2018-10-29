using System;
using ActiveTimeline.Enumerate;
using UnityEngine;
using UnityEngine.Playables;

namespace ActiveTimeline.Structure
{
    public interface IPredicate
    {
        ExposedReference<Condition.Condition> Condition { get; }
        bool CheckEveryFrame { get; }
    }

    public interface ITransitionablePredicate : IPredicate
    {
        TargetType TargetType { get; }
        double Time { get; }
        int Frame { get; }
        string Label { get; }
        ExposedReference<PlayableDirector> PlayableDirector { get; }
    }

    [Serializable]
    public struct Predicate : IPredicate
    {
        [SerializeField] private ExposedReference<Condition.Condition> condition;
        [SerializeField] private bool checkEveryFrame;
        public ExposedReference<Condition.Condition> Condition => condition;
        public bool CheckEveryFrame => checkEveryFrame;
    }

    [Serializable]
    public struct TransitionablePredicate : ITransitionablePredicate
    {
        [SerializeField] private ExposedReference<Condition.Condition> condition;
        [SerializeField] private bool checkEveryFrame;
        [SerializeField] private TargetType targetType;
        [SerializeField] private double time;
        [SerializeField] private int frame;
        [SerializeField] private string label;
        [SerializeField] private ExposedReference<PlayableDirector> playableDirector;
        public ExposedReference<Condition.Condition> Condition => condition;
        public bool CheckEveryFrame => checkEveryFrame;
        public TargetType TargetType => targetType;
        public double Time => time;
        public int Frame => frame;
        public string Label => label;
        public ExposedReference<PlayableDirector> PlayableDirector => playableDirector;

        public static TransitionablePredicate WithTime(ExposedReference<Condition.Condition> condition, double time)
        {
            return new TransitionablePredicate
            {
                targetType = TargetType.Time,
                condition = condition,
                time = time,
            };
        }

        public static TransitionablePredicate WithFrame(ExposedReference<Condition.Condition> condition, int frame)
        {
            return new TransitionablePredicate
            {
                targetType = TargetType.Frame,
                condition = condition,
                frame = frame,
            };
        }

        public static TransitionablePredicate WithClip(ExposedReference<Condition.Condition> condition, string label)
        {
            return new TransitionablePredicate
            {
                targetType = TargetType.Clip,
                condition = condition,
                label = label,
            };
        }

        public static TransitionablePredicate First(ExposedReference<Condition.Condition> condition)
        {
            return new TransitionablePredicate
            {
                targetType = TargetType.First,
                condition = condition,
            };
        }

        public static TransitionablePredicate Last(ExposedReference<Condition.Condition> condition)
        {
            return new TransitionablePredicate
            {
                targetType = TargetType.Last,
                condition = condition,
            };
        }
    }
}