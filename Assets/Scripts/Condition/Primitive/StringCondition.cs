using ActiveTimeline.Enumerate;
using ActiveTimeline.Value.Primitive;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    [AddComponentMenu("ActiveTimeline/Condition/Primitive/String", (int)ConditionType.String)]
    public class StringCondition : EqualityConditionBase<string, StringValue>
    {
    }
}
