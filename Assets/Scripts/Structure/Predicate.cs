using System;
using ActiveTimeline.Component;
using ActiveTimeline.Condition;
using ActiveTimeline.Enumerate;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Playables;

namespace ActiveTimeline.Structure
{
    public interface IPredicate
    {
        ExposedReference<ConditionBase> Condition { get; }
        bool CheckEveryFrame { get; }
    }

    public interface ITransitionablePredicate : IPredicate
    {
        TargetType TargetType { get; }
        double Time { get; }
        int Frame { get; }
        string Label { get; }
        ExposedReference<PlayableDirector> PlayableDirector { get; }
        ExposedReference<EventTrigger> EventTrigger { get; }
    }

    [Serializable][PublicAPI]
    public struct Predicate : IPredicate
    {
        [SerializeField] private ExposedReference<ConditionBase> condition;
        [SerializeField] private bool checkEveryFrame;

        public ExposedReference<ConditionBase> Condition
        {
            get { return condition; }
            set { condition = value; }
        }

        public bool CheckEveryFrame
        {
            get { return checkEveryFrame; }
            set { checkEveryFrame = value; }
        }
    }

    [Serializable][PublicAPI]
    public struct TransitionablePredicate : ITransitionablePredicate
    {
        [SerializeField] private ExposedReference<ConditionBase> condition;
        [SerializeField] private bool checkEveryFrame;
        [SerializeField] private TargetType targetType;
        [SerializeField] private double time;
        [SerializeField] private int frame;
        [SerializeField] private string label;
        [SerializeField] private ExposedReference<PlayableDirector> playableDirector;
        [SerializeField] private ExposedReference<EventTrigger> eventTrigger;

        public ExposedReference<ConditionBase> Condition
        {
            get { return condition; }
            set { condition = value; }
        }

        public bool CheckEveryFrame
        {
            get { return checkEveryFrame; }
            set { checkEveryFrame = value; }
        }

        public TargetType TargetType
        {
            get { return targetType; }
            set { targetType = value; }
        }

        public double Time
        {
            get { return time; }
            set { time = value; }
        }

        public int Frame
        {
            get { return frame; }
            set { frame = value; }
        }

        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        public ExposedReference<PlayableDirector> PlayableDirector
        {
            get { return playableDirector; }
            set { playableDirector = value; }
        }

        public ExposedReference<EventTrigger> EventTrigger
        {
            get { return eventTrigger; }
            set { eventTrigger = value; }
        }
    }
}
