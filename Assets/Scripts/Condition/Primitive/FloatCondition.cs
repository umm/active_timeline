using ActiveTimeline.Enumerate;
using ActiveTimeline.Value.Primitive;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    [AddComponentMenu("ActiveTimeline/Condition/Primitive/Float", (int)ConditionType.Float)]
    public class FloatCondition : ComparableConditionBase<float, FloatValue>
    {
    }
}
