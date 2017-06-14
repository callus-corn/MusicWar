using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.Networking;

public class PlayerInput : NetworkBehaviour ,IInputProvider{
    public IReadOnlyReactiveProperty<Vector3> Move { get { return _move; } }
    public IReadOnlyReactiveProperty<bool> Attack { get { return _attack; } }
    public IReadOnlyReactiveProperty<bool> Charge { get { return _charge; } }
    public IReadOnlyReactiveProperty<bool> Change { get { return _change; } }
    public IReadOnlyReactiveProperty<float> Turn { get { return _turn; } }
    public IReadOnlyReactiveProperty<float> CameraMove { get { return _cameraMove; } }

    private ReactiveProperty<Vector3> _move = new ReactiveProperty<Vector3>();
    private ReactiveProperty<bool> _attack = new ReactiveProperty<bool>();
    private ReactiveProperty<bool> _charge = new ReactiveProperty<bool>();
    private ReactiveProperty<bool> _change = new ReactiveProperty<bool>();
    private ReactiveProperty<float> _turn = new ReactiveProperty<float>();
    private ReactiveProperty<float> _cameraMove = new ReactiveProperty<float>();

    [SyncVar]
    private Vector3 _syncMove;
    [SyncVar]
    private bool _syncAttack;
    [SyncVar]
    private bool _syncCharge;
    [SyncVar]
    private bool _syncChange;
    [SyncVar]
    private float _syncTurn;
    [SyncVar]
    private float _syncCameraMove;


    [ClientCallback]
    void Start () {
        //PlayerInput
        this.UpdateAsObservable()
            .Where(_ => isLocalPlayer)
            .Select(_ => new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")))
            .Subscribe(move =>_move.Value = move);

        this.UpdateAsObservable()
            .Where(_ => isLocalPlayer)
            .Select(_ => Input.GetMouseButtonDown(0))
            .Where(click => click )
            .Subscribe(click => _attack.Value = !_attack.Value);

        this.UpdateAsObservable()
            .Where(_ => isLocalPlayer)
            .Select(_ => Input.GetMouseButtonDown(1))
            .Where(click => click)
            .Subscribe(click => _charge.Value = !_charge.Value);

        this.UpdateAsObservable()
            .Where(_ => isLocalPlayer)
            .Select(_ => Input.GetKeyDown(KeyCode.E))
            .Where(e => e)
            .Subscribe(e => _change.Value = !_change.Value);

        this.UpdateAsObservable()
            .Where(_ => isLocalPlayer)
            .Subscribe(_ => _turn.Value = Input.GetAxis("Mouse X"));

        this.UpdateAsObservable()
            .Where(_ => isLocalPlayer)
            .Subscribe(_ => _cameraMove.Value = Input.GetAxis("Mouse Y"));


        //Synchronize
        this.UpdateAsObservable()
            .Where(_ => isLocalPlayer)
            .Subscribe(_ => {
                CmdSynchronizeMove(_move.Value);
                CmdSynchronizeAttack(_attack.Value);
                CmdSynchronizeCharge(_charge.Value);
                CmdSynchronizeChange(_change.Value);
                CmdSynchronizeTurn(_turn.Value);
                CmdSynchronizeCameraMove(_cameraMove.Value);
            });

        //NetworkInput
        this.UpdateAsObservable()
            .Where(_ => !isLocalPlayer)
            .Subscribe(_ => _move.Value = _syncMove);

        this.UpdateAsObservable()
            .Where(_ => !isLocalPlayer)
            .Subscribe(leftClick => _attack.Value = _syncAttack);

        this.UpdateAsObservable()
            .Where(_ => !isLocalPlayer)
            .Subscribe(rightClick => _charge.Value = _syncCharge);

        this.UpdateAsObservable()
            .Where(_ => !isLocalPlayer)
            .Subscribe(e => _change.Value = _syncChange);

        this.UpdateAsObservable()
            .Where(_ => !isLocalPlayer)
            .Subscribe(_ => _turn.Value = _syncTurn);

        this.UpdateAsObservable()
            .Where(_ => !isLocalPlayer)
            .Subscribe(_ => _cameraMove.Value = _syncCameraMove);
    }

    [Command]
    void CmdSynchronizeMove(Vector3 move)
    {
        _syncMove = move;
    }

    [Command]
    void CmdSynchronizeAttack(bool attack)
    {
        _syncAttack = attack;
    }

    [Command]
    void CmdSynchronizeCharge(bool charge)
    {
        _syncCharge = charge;
    }

    [Command]
    void CmdSynchronizeChange(bool change)
    {
        _syncChange = change;
    }


    [Command]
    void CmdSynchronizeTurn(float turn)
    {
        _syncTurn = turn;
    }

    [Command]
    void CmdSynchronizeCameraMove(float cameraMove)
    {
        _syncCameraMove = cameraMove;
    }
}
