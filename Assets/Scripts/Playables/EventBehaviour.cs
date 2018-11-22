using System;
using ActiveTimeline.Component;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace ActiveTimeline.Playables
{
    [Serializable]
    [PublicAPI]
    public class EventBehaviour : ActiveBehaviourBase
    {
        [SerializeField] private ExposedReference<EventTrigger> eventTriggerReference;
        private EventTrigger eventTrigger;
        private EventTrigger EventTrigger => eventTrigger ? eventTrigger : (eventTrigger = eventTriggerReference.Resolve(ExposedPropertyTable));

        protected override void Initialize()
        {
            DoBehaviourStartAsObservable().Subscribe(_ => EventTrigger.OnStart?.Invoke());
            DoBehaviourEndAsObservable().Subscribe(_ => EventTrigger.OnEnd?.Invoke());
        }
    }
}
