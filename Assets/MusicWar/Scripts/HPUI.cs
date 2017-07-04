using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UniRx;

public class HPUI : MonoBehaviour
{
    IDamageAppliable _hp;

    public void Initialize()
    {
        var manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _hp = GameObject.Find(manager.ID).GetComponent<IDamageAppliable>();

        _hp.HP
            .Subscribe(hp =>this.GetComponent<Text>().text = hp.ToString());

    }
}