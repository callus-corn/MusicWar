using UnityEngine;
using UniRx;

public interface IInputProvider
{
    IReadOnlyReactiveProperty<Vector3> Move { get; }
    IReadOnlyReactiveProperty<bool> Attack { get; }
    IReadOnlyReactiveProperty<bool> Charge { get; }
    IReadOnlyReactiveProperty<bool> Change { get; }
    IReadOnlyReactiveProperty<float> Turn { get; }
    IReadOnlyReactiveProperty<float> CameraMove { get; }
}
