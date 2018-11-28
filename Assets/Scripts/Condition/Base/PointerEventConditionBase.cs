using UnityEngine;
using UnityEngine.EventSystems;

namespace ActiveTimeline.Condition
{
    [RequireComponent(typeof(UIBehaviour))]
    public abstract class PointerEventConditionBase : ConditionBase
    {
        private UIBehaviour uiBehaviour;
        protected UIBehaviour UIBehaviour => uiBehaviour != null ? uiBehaviour : uiBehaviour = GetComponent<UIBehaviour>();
    }
}
