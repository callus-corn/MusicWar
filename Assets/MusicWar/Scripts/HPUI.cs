using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UniRx;

public class HPUI : MonoBehaviour
{
    GameObject _localPlayer;
    IDamageAppliable _hp;

    public void Initialize()
    {
        var manager = GameObject.Find("GameManager").GetComponent<GameManager>();

//        _hp = manager.Player.GetComponent<IDamageAppliable>();

        this.GetComponent<Text>().text = _hp.HP.Value.ToString();

        _hp.HP
            .Subscribe(hp => this.GetComponent<Text>().text = hp.ToString());

    }
}