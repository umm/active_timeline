using System;
using ActiveTimeline.Enumerate;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace ActiveTimeline.Condition
{
    [AddComponentMenu("ActiveTimeline/Condition/PointerEvent/Drag", (int)ConditionType.Drag)]
    public class DragCondition : PointerEventConditionBase
    {
        [SerializeField] private float thresholdX;
        [SerializeField] private float thresholdY;
        [SerializeField] private bool summarizeAbsolutely;
        [SerializeField] private bool resetOnEndDrag;
        private float ThresholdX => thresholdX;
        private float ThresholdY => thresholdY;
        private bool SummarizeAbsolutely => summarizeAbsolutely;
        private bool ResetOnEndDrag => resetOnEndDrag;
        private IReactiveProperty<Vector2> DeltaProperty { get; } = new Vector2ReactiveProperty();

        private void Start()
        {
            // IDragHandler が居ないと反応しないっぽいので、無理矢理くっつける
            UIBehaviour.OnDragAsObservable().Take(1).Subscribe();
            UIBehaviour.OnBeginDragAsObservable()
                .SelectMany(_ => UIBehaviour.OnDragAsObservable())
                .Subscribe(
                    // 絶対値加算の場合は Mathf.Abs して加算
                    pointerEventData => DeltaProperty.Value = new Vector2(
                        DeltaProperty.Value.x + (SummarizeAbsolutely ? Mathf.Abs(pointerEventData.delta.x) : pointerEventData.delta.x),
                        DeltaProperty.Value.y + (SummarizeAbsolutely ? Mathf.Abs(pointerEventData.delta.y) : pointerEventData.delta.y)
                    )
                );
            // ドラッグ終了でリセット
            if (ResetOnEndDrag)
            {
                UIBehaviour.OnEndDragAsObservable().Subscribe(_ => DeltaProperty.Value = Vector2.zero);
            }
        }

        public override IObservable<Unit> OnFulfilledAsObservable()
        {
            return DeltaProperty
                .Where(
                    delta =>
                        // 閾値が0であるか、閾値よりも累積差分が大きい場合に通す
                        (
                            Mathf.Approximately(ThresholdX, 0.0f) ||
                            (int) Mathf.Sign(ThresholdX) == (int) Mathf.Sign(delta.x) && Mathf.Abs(ThresholdX) < Mathf.Abs(delta.x)
                        ) &&
                        (
                            Mathf.Approximately(ThresholdY, 0.0f) ||
                            (int) Mathf.Sign(ThresholdY) == (int) Mathf.Sign(delta.y) && Mathf.Abs(ThresholdY) < Mathf.Abs(delta.y)
                        )
                )
                .AsUnitObservable();
        }
    }
}
