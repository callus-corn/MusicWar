using UnityEngine;
using UnityEngine.Networking;
using UniRx;
using UniRx.Triggers;

public class NetworkTransform : NetworkBehaviour
{
    [SyncVar]
    private Vector3 _syncPosition;
    [SyncVar]
    private Quaternion _syncRotation;

	void Start ()
    {
        this.UpdateAsObservable()
            .Where(_ => isLocalPlayer)
            .Subscribe(_ => CmdSynchronizePosition(transform.position));

        this.UpdateAsObservable()
            .Where(_ => isLocalPlayer)
            .Subscribe(_ => CmdSynchronizeRotetion(transform.rotation));

        this.UpdateAsObservable()
            .Where(_ => !isLocalPlayer)
            .Subscribe(_ => transform.position += (_syncPosition - transform.position)*Time.deltaTime);

        this.UpdateAsObservable()
            .Where(_ => !isLocalPlayer)
            .Subscribe(_ => transform.rotation = _syncRotation);
    }

    [Command]
    void CmdSynchronizePosition(Vector3 position)
    {
        _syncPosition = position;
    }

    [Command]
    void CmdSynchronizeRotetion(Quaternion rotation)
    {
        _syncRotation = rotation;
    }

}
