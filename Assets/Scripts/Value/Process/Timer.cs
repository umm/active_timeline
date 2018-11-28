using ActiveTimeline.Enumerate;
using ActiveTimeline.Value.Primitive;
using JetBrains.Annotations;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace ActiveTimeline.Value.Process
{
    [PublicAPI]
    [AddComponentMenu("ActiveTimeline/Value/Process/Timer", (int)ValueType.Timer)]
    public class Timer : FloatValue
    {
        [SerializeField] private bool playOnAwake;
        private bool IsPlaying { get; set; }

        private void Awake()
        {
            IsPlaying = playOnAwake;
        }

        private void Start()
        {
            this
                .UpdateAsObservable()
                .Where(_ => IsPlaying)
                .Subscribe(_ => Value += Time.deltaTime)
                .AddTo(this);
        }

        public void StartTimer()
        {
            ResetTimer();
            IsPlaying = true;
        }

        public void StopTimer()
        {
            ResetTimer();
            IsPlaying = false;
        }

        public void PauseTimer()
        {
            IsPlaying = false;
        }

        public void ResumeTimer()
        {
            IsPlaying = true;
        }

        public void ResetTimer()
        {
            Value = 0.0f;
        }
    }
}
