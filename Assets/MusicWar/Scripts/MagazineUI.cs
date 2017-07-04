using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UniRx;

public class MagazineUI : MonoBehaviour
{
    IBulletUsable _magazine;

    public void Initialize()
    {
        var manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _magazine = GameObject.Find(manager.ID).transform.Find("Wepon").gameObject.GetComponent<IBulletUsable>();

        this.GetComponent<Text>().text = _magazine.Bullets.Value.ToString();

        _magazine.Bullets
            .Subscribe(magazine => this.GetComponent<Text>().text = magazine.ToString());
            
    }
}