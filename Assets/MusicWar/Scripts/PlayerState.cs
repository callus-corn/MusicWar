using UnityEngine;
using UniRx;

public class PlayerState : MonoBehaviour,IStateProvider ,IDamageAppliable , IBulletUsable
{
    public IReadOnlyReactiveProperty<float> HP { get { return _hp; } }
    public IReadOnlyReactiveProperty<float> Magazine { get { return _magazine; } }

    private ReactiveProperty<float> _hp = new ReactiveProperty<float>(100);
    private ReactiveProperty<float> _magazine = new ReactiveProperty<float>(5);

    void Start ()
    {
        _hp.Where(hp => hp <= 0)
           .Subscribe(hp => Debug.Log("Death"));
	}

    public void ApplyDamage(Damage damage)
    {
        _hp.Value -= damage.Value;
    }

    public void UseBullet(float amount)
    {
        _magazine.Value -= amount;
    }

    public void ChargeBullet(float amount)
    {
        _magazine.Value += amount;
    }
}
