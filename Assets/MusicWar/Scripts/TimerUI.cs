using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class TimerUI : MonoBehaviour
{

    float _time = 300;

    public void Initialize()
    {
        this.UpdateAsObservable()
            .Subscribe(_ => this.GetComponent<Text>().text = ((int)_time/60).ToString() + ":" + ((int)_time%60).ToString() );
        this.UpdateAsObservable()
            .Subscribe(_ => _time -= Time.deltaTime);
    }
}
