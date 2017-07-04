using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Timer : MonoBehaviour
{
    public IReadOnlyReactiveProperty<float> Time { get { return _time; } }
    ReactiveProperty<float> _time = new ReactiveProperty<float>();

    public void Initialize()
    {
        _time.Value = 30;
        this.UpdateAsObservable()
            .Where(_ => _time.Value >= 0)
            .Subscribe(_ => _time.Value -= UnityEngine.Time.deltaTime);
    }
}
