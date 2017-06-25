using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;


public class MatchingController : MonoBehaviour
{
    private void Start()
    {
        var _manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _manager.Ready();

        this.UpdateAsObservable()
            .Subscribe(_ => {
                var text = GameObject.Find("Canvas/Text").GetComponent<Text>();
                var _matchingUser = _manager.matching.users;

                text.text = string.Join("\n",_matchingUser);
            });
    }
}
