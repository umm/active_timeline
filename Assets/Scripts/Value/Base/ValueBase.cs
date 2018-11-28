using UniRx;
using UnityEngine;

namespace ActiveTimeline.Value
{
    public abstract class ValueBase<T> : MonoBehaviour, IValue<T>
    {
        [SerializeField] private T value;
        private ReactiveProperty<T> reactiveProperty;

        public T Value
        {
            get { return ReactiveProperty.Value; }
            set { this.value = value; }
        }

        public IReactiveProperty<T> ReactiveProperty => reactiveProperty ?? (reactiveProperty = new ReactiveProperty<T>(value));

        private void Start()
        {
            this
                .ObserveEveryValueChanged(x => x.value)
                .Subscribe(x => ReactiveProperty.Value = x);
        }
    }
}
