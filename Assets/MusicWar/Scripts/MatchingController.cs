using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System;


public class MatchingController : MonoBehaviour
{
    private void Start()
    {
        var _manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        var _text = GameObject.Find("Canvas/Text").GetComponent<Text>();

        _manager.StartMatching();

        this.UpdateAsObservable()
            .Subscribe(_ => _text.text = string.Join("\n", _manager.MatchingUsers) );
    }

}
