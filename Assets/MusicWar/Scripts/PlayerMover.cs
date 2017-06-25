using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerMover : MonoBehaviour {
    IInputProvider _input;

    const float _turnSpeed = 3;
    const float _moveSpeed = 0.1f;

    public void Initialize()
    {
        _input = this.GetComponent<IInputProvider>();

        _input.Turn
            .Subscribe(turn => transform.Rotate(0, _turnSpeed * turn, 0));

        this.UpdateAsObservable()
            .Select(_ => _input.Move.Value)
            .Subscribe(move => transform.Translate(move * _moveSpeed));
    }
}
