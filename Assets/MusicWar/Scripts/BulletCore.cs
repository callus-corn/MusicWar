using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class BulletCore : MonoBehaviour
{
    private void Start()
    {
        this.OnTriggerEnterAsObservable()
            .Subscribe(collision => {
                var applyDamage = collision.gameObject.GetComponent<IDamageAppliable>();
                applyDamage.ApplyDamage(new Damage());
                Destroy(this.gameObject);
            });
    }

}
