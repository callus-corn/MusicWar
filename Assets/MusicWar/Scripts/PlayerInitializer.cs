using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerInitializer : MonoBehaviour, IInitializable
{
    public Vector3 startPosition;
    public Quaternion startRotetion;

    public void Initialize()
    {
        Debug.Log("Initializer");
        this.transform.position = startPosition;
        this.transform.rotation = startRotetion;

        this.GetComponent<PlayerInput>().Initialize();
        this.GetComponent<NetworkTransform>().Initialize();
        this.GetComponent<PlayerMover>().Initialize();
        this.GetComponent<PlayerAnimator>().Initialize();
        this.GetComponent<PlayerAttacker>().Initialize();
        this.GetComponent<PlayerState>().Initialize();
        this.GetComponent<PlayerHealth>().Initialize();

        this.GetComponent<PlayerReInitializer>().startPosition = startPosition;
        this.GetComponent<PlayerReInitializer>().startRotetion = startRotetion;
        this.GetComponent<PlayerReInitializer>().Initialize();
    }
}
