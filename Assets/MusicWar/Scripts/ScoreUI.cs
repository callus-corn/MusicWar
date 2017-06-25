using UnityEngine;
using UnityEngine.Networking;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    GameObject _localPlayer;
    int _score =0;
    IInputProvider _input;

    public void Initialize()
    {
        var manager = GameObject.Find("GameManager").GetComponent<GameManager>();
 //       _input = manager.Player.GetComponent<IInputProvider>();

        _input.Attack
            .Subscribe(_ => _score++);

        _input.Charge
            .Subscribe(_ => _score++);

        this.UpdateAsObservable()
            .Subscribe(_ => this.GetComponent<Text>().text = "score : " + _score.ToString());
    }
}
