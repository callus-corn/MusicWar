using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.Networking;

public class PlayerInput : NetworkBehaviour ,IInputProvider{
    public IReadOnlyReactiveProperty<Vector3> Move { get { return _move; } }
    private ReactiveProperty<Vector3> _move = new ReactiveProperty<Vector3>();

    [SyncVar]
    private Vector3 _syncMove;

    [ClientCallback]
    void Start () {
        this.UpdateAsObservable()
            .Where(_ => isLocalPlayer)
            .Select(_ => new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")))
            .Subscribe(move =>{
                _move.Value = move;
                CmdSynchronizeMove(move);
            });

        this.UpdateAsObservable()
            .Where(_ => !isLocalPlayer)
            .Select(_ => _syncMove)
            .Subscribe(move => _move.Value = move);
    }

    [Command]
    void CmdSynchronizeMove(Vector3 move)
    {
        _syncMove = move;
    }
}
