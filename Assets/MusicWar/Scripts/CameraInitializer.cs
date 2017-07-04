using UnityEngine;

public class CameraInitializer : MonoBehaviour,IInitializable
{
    public void Initialize()
    {
        this.gameObject.SetActive(true);
        this.GetComponent<CameraMover>().Initialize();
    }
}
