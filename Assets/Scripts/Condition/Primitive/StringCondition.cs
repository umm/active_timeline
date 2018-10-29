using ActiveTimeline.Enumerate;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    [AddComponentMenu("ActiveTimeline/Primitive/String", (int)ConditionType.String)]
    public class StringCondition : AssertCondition<string>
    {
    }
}