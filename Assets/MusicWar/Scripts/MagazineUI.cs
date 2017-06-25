using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UniRx;

public class MagazineUI : MonoBehaviour
{
    GameObject _localPlayer;
    IBulletUsable _magazine;

    public void Initialize()
    {
        var manager = GameObject.Find("GameManager").GetComponent<GameManager>();
//        _magazine = manager.Player.transform.Find("Wepon").gameObject.GetComponent<IBulletUsable>();

        this.GetComponent<Text>().text = _magazine.Bullets.Value.ToString();

        _magazine.Bullets
            .Subscribe(magazine => this.GetComponent<Text>().text = magazine.ToString());
            
    }
}