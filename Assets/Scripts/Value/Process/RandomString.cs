using System.Collections.Generic;
using System.Linq;
using ActiveTimeline.Enumerate;
using ActiveTimeline.Value.Primitive;
using ExtraLinq;
using JetBrains.Annotations;
using UnityEngine;

namespace ActiveTimeline.Value.Process
{
    [PublicAPI]
    [AddComponentMenu("ActiveTimeline/Value/Process/Random String", (int)ValueType.RandomString)]
    public class RandomString : StringValue
    {
        [SerializeField] private List<string> elementList;
        private IEnumerable<string> ElementList => elementList;

        private void Awake()
        {
            Refresh();
        }

        public void Refresh()
        {
            if (ElementList.Any())
            {
                Value = ElementList.Random();
            }
        }
    }
}
