using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class BulletDamage : MonoBehaviour
{
    public Damage Value { get; set; }

	void Start ()
    {
        this.OnTriggerEnterAsObservable()
            .Where(collision => collision.gameObject.GetComponent<IDamageAppliable>() != null)
            .Select(collision => collision.gameObject.GetComponent<IDamageAppliable>())
            .Subscribe(collision => collision.ApplyDamage(Value));
    }
	
}
