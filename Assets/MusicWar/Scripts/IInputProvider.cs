using UnityEngine;
using UniRx;
using UnityEngine.Networking;

public interface IInputProvider {
    IReadOnlyReactiveProperty<Vector3> Move { get; }
    IReadOnlyReactiveProperty<bool> LeftClick { get; }
}
