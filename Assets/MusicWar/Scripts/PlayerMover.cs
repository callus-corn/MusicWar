using UnityEngine;
using UniRx;

public class PlayerMover : MonoBehaviour {
    IInputProvider _input;
    Animator _animator;

    private void Awake()
    {
        _input = this.GetComponent<IInputProvider>();
        _animator = this.GetComponent<Animator>();
    }

    void Start () {
        _input.Move
            .Where(move => move.magnitude > 0.1)
            .Select(move => Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg - transform.localEulerAngles.y)
            .Subscribe(turn => transform.Rotate(0,turn,0));

        _input.Move
            .Subscribe(move => _animator.SetFloat("Run", move.magnitude));

        _input.LeftClick
            .Subscribe(leftClick => transform.Rotate(0, 90, 0));
    }
}
