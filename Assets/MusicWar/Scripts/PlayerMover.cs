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
            .Subscribe(move => {
                var turn = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg - transform.localEulerAngles.y;
                transform.Rotate(0,turn,0);
                _animator.SetFloat("Run",move.magnitude);
            });
	}
}
