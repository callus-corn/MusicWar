using UnityEngine;
using UniRx;

public class PlayerMover : MonoBehaviour {
    IInputProvider _input;
    Animator _animator;

    const float _turnSpeed = 3;

    private void Awake()
    {
        _input = this.GetComponent<IInputProvider>();
        _animator = this.GetComponent<Animator>();
    }

    void Start () {
        _input.Turn
            .Subscribe(turn => transform.Rotate(0,_turnSpeed*turn,0));

        _input.Move
            .Subscribe(move => _animator.SetFloat("Run", move.z));
    }
}
