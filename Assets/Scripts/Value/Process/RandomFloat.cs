using ActiveTimeline.Enumerate;
using ActiveTimeline.Value.Primitive;
using JetBrains.Annotations;
using UnityEngine;

namespace ActiveTimeline.Value.Process
{
    [PublicAPI]
    [AddComponentMenu("ActiveTimeline/Value/Process/Random Float", (int)ValueType.RandomFloat)]
    public class RandomFloat : FloatValue
    {
        [SerializeField] private float min;
        [SerializeField] private float max;
        private float Min => min;
        private float Max => max;

        private void Awake()
        {
            Refresh();
        }

        public void Refresh()
        {
            Value = Random.Range(Min, Max);
        }
    }
}
