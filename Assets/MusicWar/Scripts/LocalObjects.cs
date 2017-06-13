using UnityEngine;
using UnityEngine.Networking;

public class LocalObjects : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] behaviours;

    void Start()
    {
        if (!isLocalPlayer)
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.enabled = false;
            }
        }
    }
}
