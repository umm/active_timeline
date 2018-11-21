using UniRx;

namespace ActiveTimeline
{
    public interface IValue<T>
    {
        IReactiveProperty<T> ReactiveProperty { get; }
    }
}
