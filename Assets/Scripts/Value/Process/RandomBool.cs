using ActiveTimeline.Enumerate;
using ActiveTimeline.Value.Primitive;
using JetBrains.Annotations;
using UnityEngine;

namespace ActiveTimeline.Value.Process
{
    [PublicAPI]
    [AddComponentMenu("ActiveTimeline/Value/Process/Random Bool", (int)ValueType.RandomBool)]
    public class RandomBool : BoolValue
    {
        private void Awake()
        {
            Refresh();
        }

        public void Refresh()
        {
            Value = Random.Range(0, 2) == 1;
        }
    }
}
