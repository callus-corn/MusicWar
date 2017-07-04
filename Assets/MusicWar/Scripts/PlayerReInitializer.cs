using UnityEngine;
using UniRx;

public class PlayerReInitializer : MonoBehaviour
{
    public Vector3 startPosition;
    public Quaternion startRotetion;

    public void Initialize()
    {
        this.GetComponent<PlayerState>().Respown
            .Subscribe(_ => {
                this.transform.position = startPosition;
                this.transform.rotation = startRotetion;
                this.GetComponent<PlayerState>().Initialize();
                this.GetComponent<PlayerHealth>().Initialize();
            });
    }

}
