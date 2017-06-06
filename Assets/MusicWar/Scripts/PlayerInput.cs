using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerInput : MonoBehaviour ,IInputProvider{
    public IReadOnlyReactiveProperty<Vector3> Move { get { return _move; } }
    private ReactiveProperty<Vector3> _move = new ReactiveProperty<Vector3>();

	void Start () {
        this.UpdateAsObservable()
            .Select(_ => new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")))
            .Subscribe(move => _move.Value = move);
	}
}
