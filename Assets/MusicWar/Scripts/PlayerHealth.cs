using UnityEngine;
using UniRx;

public class PlayerHealth : MonoBehaviour ,IDamageAppliable
{
    public IReadOnlyReactiveProperty<float> HP { get { return _hp; } }

    private ReactiveProperty<float> _hp = new ReactiveProperty<float>(100);

    void Start ()
    {
        _hp.Where(hp => hp <= 0)
           .Subscribe(hp => Debug.Log("Death"));
	}

    public void ApplyDamage(Damage damage)
    {
        _hp.Value -= damage.Value;
    }
}
