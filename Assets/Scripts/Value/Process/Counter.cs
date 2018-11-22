using ActiveTimeline.Enumerate;
using ActiveTimeline.Value.Primitive;
using JetBrains.Annotations;
using UnityEngine;

namespace ActiveTimeline.Value.Process
{
    [PublicAPI]
    [AddComponentMenu("ActiveTimeline/Value/Process/Counter", (int)ValueType.Counter)]
    public class Counter : IntValue
    {
        public void Increment()
        {
            Value++;
        }

        public void Decrement()
        {
            Value--;
        }
    }
}
