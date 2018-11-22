using System;
using UnityEngine;
using UnityEngine.Events;

namespace ActiveTimeline.Component
{
    [AddComponentMenu("ActiveTimeline/Component/EventTrigger")]
    public class EventTrigger : MonoBehaviour
    {
        [SerializeField] private OnTriggerEvent onTrigger = new OnTriggerEvent();
        public OnTriggerEvent OnTrigger => onTrigger;

        [Serializable]
        public class OnTriggerEvent : UnityEvent
        {
        }
    }
}
