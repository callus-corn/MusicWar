using UnityEngine;
using UniRx;

public interface IInputProvider {
    IReadOnlyReactiveProperty<Vector3> Move { get; }
}
