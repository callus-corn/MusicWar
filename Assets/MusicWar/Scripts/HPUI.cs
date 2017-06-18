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
        _localPlayer = ClientScene.localPlayers[0].gameObject;
        _hp = _localPlayer.GetComponent<IDamageAppliable>();

        this.GetComponent<Text>().text = _hp.HP.Value.ToString();

        _hp.HP
            .Subscribe(hp => this.GetComponent<Text>().text = hp.ToString());

    }
}