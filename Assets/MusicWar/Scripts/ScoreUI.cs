using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    Score _score;

    public void Initialize()
    {
        _score = GameObject.Find("GameController").GetComponent<Score>();

        _score.PlayerScore
            .Subscribe(score => this.GetComponent<Text>().text = "score : " + score.ToString());
    }
}
