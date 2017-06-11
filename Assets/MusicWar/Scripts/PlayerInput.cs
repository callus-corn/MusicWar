using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.Networking;

public class PlayerInput : NetworkBehaviour ,IInputProvider{
    public IReadOnlyReactiveProperty<Vector3> Move { get { return _move; } }
    public IReadOnlyReactiveProperty<bool> LeftClick { get { return _leftClick; } }

    private ReactiveProperty<Vector3> _move = new ReactiveProperty<Vector3>();
    private ReactiveProperty<bool> _leftClick = new ReactiveProperty<bool>();

    [SyncVar]
    private Vector3 _syncMove;
    [SyncVar]
    private bool _syncLeftClick;

    [ClientCallback]
    void Start () {
        //PlayerInput
        this.UpdateAsObservable()
            .Where(_ => isLocalPlayer)
            .Select(_ => new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")))
            .Subscribe(move =>_move.Value = move);

        this.UpdateAsObservable()
            .Where(_ => isLocalPlayer)
            .Where(_ => Input.GetMouseButtonDown(0))
            .Subscribe(_ => _leftClick.Value = !_leftClick.Value);

        //Synchronize
        this.UpdateAsObservable()
            .Where(_ => isLocalPlayer)
            .Subscribe(_ => {
                CmdSynchronizeMove(_move.Value);
                CmdSynchronizeLeftClick(_leftClick.Value);
            });

        //NetworkInput
        this.UpdateAsObservable()
            .Where(_ => !isLocalPlayer)
            .Subscribe(_ => _move.Value = _syncMove);

        this.UpdateAsObservable()
            .Where(_ => !isLocalPlayer)
            .Subscribe(leftClick => _leftClick.Value = _syncLeftClick);
    }

    [Command]
    void CmdSynchronizeMove(Vector3 move)
    {
        _syncMove = move;
    }

    [Command]
    void CmdSynchronizeLeftClick(bool leftClick)
    {
        _syncLeftClick = leftClick;
    }
}
