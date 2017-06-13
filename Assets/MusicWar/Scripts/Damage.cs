using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Damage : MonoBehaviour , IDamage
{
    public IStateProvider Attacker { get { return _attacker; } set { _attacker = value; } }
    public float Value { get { return _value; } set { _value = value; } }

    private IStateProvider _attacker;
    private float _value;

    private void Start()
    {
        this.OnTriggerEnterAsObservable()
            .Subscribe(collision =>{
                var hit = collision.gameObject.GetComponent<IDamageAppliable>();
                hit.ApplyDamage(this);
            });
    }
}
