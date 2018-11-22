using System;
using UnityEngine;
using UnityEngine.Events;

namespace ActiveTimeline.Component
{
    [AddComponentMenu("ActiveTimeline/Component/EventTrigger")]
    public class EventTrigger : MonoBehaviour
    {
        [SerializeField] private OnStartEvent onStart = new OnStartEvent();
        [SerializeField] private OnEndEvent onEnd = new OnEndEvent();
        public OnStartEvent OnStart => onStart;
        public OnEndEvent OnEnd => onEnd;

        [Serializable]
        public class OnStartEvent : UnityEvent
        {
        }

        [Serializable]
        public class OnEndEvent : UnityEvent
        {
        }
    }
}
