using UnityEngine;
using UnityEngine.EventSystems;

namespace ActiveTimeline.Condition
{
    [RequireComponent(typeof(UIBehaviour))]
    public abstract class PointerEventCondition : Condition
    {
        private UIBehaviour uiBehaviour;
        protected UIBehaviour UIBehaviour => uiBehaviour != null ? uiBehaviour : uiBehaviour = GetComponent<UIBehaviour>();
    }
}