using UnityEngine;
using UniRx;

public class PlayerAnimator : MonoBehaviour {
    IInputProvider _input;
    IStateProvider _state;
    Animator _animator;

    private void Awake()
    {
        _input = this.GetComponent<IInputProvider>();
        _state = this.GetComponent<IStateProvider>();
        _animator = this.GetComponent<Animator>();
    }

    void Start ()
    {
        _input.Move
            .Subscribe(move => _animator.SetFloat("Run", move.magnitude));

        _input.Move
            .Subscribe(_ => _state.ToRunning());
    }	
}
