using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class TimerUI : MonoBehaviour
{

    Timer timer;

    public void Initialize()
    {
        timer = GameObject.Find("GameController").GetComponent<Timer>();

        timer.Time
            .Subscribe(time => this.GetComponent<Text>().text = ((int)time / 60).ToString() + ":" + ((int)time % 60).ToString());
    }
}
