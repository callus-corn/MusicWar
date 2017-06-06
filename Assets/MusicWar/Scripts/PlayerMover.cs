using UnityEngine;
using UniRx;

public class PlayerMover : MonoBehaviour {

	void Start () {
        var input = this.GetComponent<IInputProvider>();
        var animator = this.GetComponent<Animator>();


        input.Move
            .Subscribe(move => {
                var turn = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg - transform.localEulerAngles.y;
                transform.Rotate(0,turn,0);
                animator.SetFloat("Run",move.magnitude);
            });
	}
	
}
