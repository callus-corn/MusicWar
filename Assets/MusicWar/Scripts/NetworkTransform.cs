using UnityEngine;
using UnityEngine.Networking;
using UniRx;
using UniRx.Triggers;

public class NetworkTransform : NetworkBehaviour
{
    Transform _camera;

    [SyncVar]
    Vector3 _syncCameraPosition;
    [SyncVar]
    Quaternion _syncCameraRotation;

    [SyncVar]
    Vector3 _syncPosition;
    [SyncVar]
    Quaternion _syncRotation;

    public void Initialize()
    {
        _camera = transform.Find("PlayerCamera").transform;

        this.UpdateAsObservable()
            .Where(_ => isLocalPlayer)
            .Subscribe(_ => CmdSynchronizePosition(transform.position));

        this.UpdateAsObservable()
            .Where(_ => isLocalPlayer)
            .Subscribe(_ => CmdSynchronizeRotation(transform.rotation));

        this.UpdateAsObservable()
            .Where(_ => isLocalPlayer)
            .Subscribe(_ => CmdSynchronizeCameraPosition(_camera.position));

        this.UpdateAsObservable()
            .Where(_ => isLocalPlayer)
            .Subscribe(_ => CmdSynchronizeCameraRotation(_camera.rotation));


        this.UpdateAsObservable()
            .Where(_ => !isLocalPlayer)
            .Subscribe(_ => transform.position = Vector3.Lerp(transform.position, _syncPosition, Time.deltaTime));

        this.UpdateAsObservable()
            .Where(_ => !isLocalPlayer)
            .Subscribe(_ => transform.rotation = Quaternion.RotateTowards(transform.rotation, _syncRotation, Time.deltaTime * 360));

        this.UpdateAsObservable()
            .Where(_ => !isLocalPlayer)
            .Subscribe(_ => _camera.position = Vector3.Lerp(_camera.position, _syncCameraPosition, Time.deltaTime));

        this.UpdateAsObservable()
            .Where(_ => !isLocalPlayer)
            .Subscribe(_ => _camera.rotation = Quaternion.RotateTowards(_camera.rotation, _syncCameraRotation, Time.deltaTime * 360));
    }

    [Command]
    void CmdSynchronizePosition(Vector3 position)
    {
        _syncPosition = position;
    }

    [Command]
    void CmdSynchronizeRotation(Quaternion rotation)
    {
        _syncRotation = rotation;
    }

    [Command]
    void CmdSynchronizeCameraPosition(Vector3 position)
    {
        _syncCameraPosition = position;
    }

    [Command]
    void CmdSynchronizeCameraRotation(Quaternion rotation)
    {
        _syncCameraRotation = rotation;
    }
}
