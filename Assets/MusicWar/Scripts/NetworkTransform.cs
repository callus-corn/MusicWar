using UnityEngine;
using UnityEngine.Networking;
using UniRx;
using UniRx.Triggers;

public class NetworkTransform : NetworkBehaviour
{
    [SyncVar]
    private Vector3 _syncPosition;

	void Start ()
    {
        this.UpdateAsObservable()
            .Where(_ => isLocalPlayer)
            .Subscribe(_ => CmdSynchronizePosition(transform.position));

        this.UpdateAsObservable()
            .Where(_ => !isLocalPlayer)
            .Subscribe(_ => transform.position += (_syncPosition - transform.position)*0.016f);

    }

    [Command]
    void CmdSynchronizePosition(Vector3 position)
    {
        _syncPosition = position;
    }

}
