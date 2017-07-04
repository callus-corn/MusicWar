using UnityEngine;

public class WeponInitializer : MonoBehaviour,IInitializable
{
    public void Initialize()
    {
        this.GetComponent<BaseWepon>().Initialize();
    }
}
