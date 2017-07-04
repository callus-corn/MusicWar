using UnityEngine;

public class GameControllerInitializer : MonoBehaviour,IInitializable
{
    public void Initialize()
    {
        this.GetComponent<Score>().Initialize();
        this.GetComponent<Timer>().Initialize();
    }
}
