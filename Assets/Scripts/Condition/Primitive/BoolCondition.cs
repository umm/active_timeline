using ActiveTimeline.Enumerate;
using ActiveTimeline.Value.Primitive;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    [AddComponentMenu("ActiveTimeline/Condition/Primitive/Bool", (int)ConditionType.Bool)]
    public class BoolCondition : EqualityConditionBase<bool, BoolValue>
    {
    }
}
