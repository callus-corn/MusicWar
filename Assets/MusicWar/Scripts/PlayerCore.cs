using UnityEngine;

public class PlayerCore : MonoBehaviour ,IDamageAppliable{

    public void ApplyDamage(Damage damage)
    {
        Debug.Log("OK");
    }
}
