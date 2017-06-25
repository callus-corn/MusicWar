using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UniRx;
using UniRx.Triggers;

public class LocalObjects : NetworkBehaviour
{
    List<Behaviour> behaviours;

    public void Initialize()
    {

        var _camera = transform.Find("PlayerCamera");

        Debug.Log(transform);
        Debug.Log(_camera);
        Debug.Log(_camera.GetComponent<Camera>());

        behaviours.Add(_camera.GetComponent<Camera>());
        behaviours.Add(_camera.GetComponent<AudioListener>());

        this.UpdateAsObservable()
            .Where(_ => !isLocalPlayer)
            .Subscribe(_ => behaviours.ForEach(behaviour => behaviour.enabled = false));
    }

}
