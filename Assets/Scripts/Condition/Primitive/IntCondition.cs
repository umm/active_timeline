using ActiveTimeline.Enumerate;
using ActiveTimeline.Value.Primitive;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    [AddComponentMenu("ActiveTimeline/Condition/Primitive/Int", (int)ConditionType.Int)]
    public class IntCondition : EqualityConditionBase<int, IntValue>
    {
    }
}
