using System;
using System.Collections.Generic;
using ActiveTimeline.Enumerate;
using ActiveTimeline.Structure;
using ExtraUniRx;
using UniRx;
using UnityEngine;
using UnityEngine.Playables;

namespace ActiveTimeline.Playables
{
    [Serializable]
    public abstract class ActiveBehaviourBase : PlayableBehaviour
    {
        public string Label { protected get; set; }
        protected ActiveMixerBehaviour ActiveMixerBehaviour { get; private set; }
        protected IExposedPropertyTable ExposedPropertyTable { get; private set; }
        private ITenseSubject Start { get; } = new TenseSubject();
        private ITenseSubject End { get; } = new TenseSubject();

        public override void OnGraphStart(Playable playable)
        {
            ActiveMixerBehaviour = ((ScriptPlayable<ActiveMixerBehaviour>) playable.GetOutput(0)).GetBehaviour();
            ExposedPropertyTable = playable.GetGraph().GetResolver();
            if (!string.IsNullOrEmpty(Label) && ActiveMixerBehaviour.MarkerMap.ContainsKey(Label))
            {
                DidBehaviourStartAsObservable()
                    .Subscribe(_ => SetProcessing(true));
                DidBehaviourEndAsObservable()
                    .Subscribe(_ => SetProcessing(false));
                ActiveMixerBehaviour
                    .TimeProperty
                    // Do not use streamed value
                    .Subscribe(_ => ProcessTime());
            }

            Initialize();
        }

        protected IObservable<Unit> WillBehaviourStartAsObservable()
        {
            return Start.WhenWill();
        }

        protected IObservable<Unit> DoBehaviourStartAsObservable()
        {
            return Start.WhenDo();
        }

        protected IObservable<Unit> DidBehaviourStartAsObservable()
        {
            return Start.WhenDid();
        }

        protected IObservable<Unit> WillBehaviourEndAsObservable()
        {
            return End.WhenWill();
        }

        protected IObservable<Unit> DoBehaviourEndAsObservable()
        {
            return End.WhenDo();
        }

        protected IObservable<Unit> DidBehaviourEndAsObservable()
        {
            return End.WhenDid();
        }

        protected void JumpTo(ITransitionablePredicate predicate)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (predicate.TargetType)
            {
                case TargetType.None:
                    break;
                case TargetType.Time:
                    ActiveMixerBehaviour.SetTime(predicate.Time);
                    break;
                case TargetType.Frame:
                    ActiveMixerBehaviour.SetTime(predicate.Frame / ActiveMixerBehaviour.FrameRate);
                    break;
                case TargetType.Clip:
                    if (!ActiveMixerBehaviour.MarkerMap.ContainsKey(predicate.Label))
                    {
                        throw new KeyNotFoundException($"Clip `{predicate.Label}' does not found in current ActiveTrack");
                    }
                    ActiveMixerBehaviour.SetTime(ActiveMixerBehaviour.MarkerMap[predicate.Label].StartTime);
                    break;
                case TargetType.First:
                    ActiveMixerBehaviour.SetTime(0);
                    break;
                case TargetType.Last:
                    ActiveMixerBehaviour.SetTime(ActiveMixerBehaviour.GetDuration());
                    break;
                case TargetType.PlayableDirector:
                    predicate.PlayableDirector.Resolve(ExposedPropertyTable).Play();
                    break;
                case TargetType.Event:
                    predicate.EventTrigger.Resolve(ExposedPropertyTable).OnTrigger.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected abstract void Initialize();

        protected void SetProcessing(bool processing)
        {
            ActiveMixerBehaviour.SetProcessing(Label, processing);
        }

        protected bool IsContainTime()
        {
            return ActiveMixerBehaviour.IsContainTime(Label);
        }

        protected bool IsProcessing()
        {
            return ActiveMixerBehaviour.IsProcessing(Label);
        }

        private void ProcessTime()
        {
            if (IsContainTime() && !IsProcessing())
            {
                Start.Will();
            }

            if (IsContainTime() && !IsProcessing())
            {
                Start.Do();
            }

            if (IsContainTime() && !IsProcessing())
            {
                Start.Did();
            }

            if (!IsContainTime() && IsProcessing())
            {
                End.Will();
            }

            if (!IsContainTime() && IsProcessing())
            {
                End.Do();
            }

            if (!IsContainTime() && IsProcessing())
            {
                End.Did();
            }
        }
    }
}
