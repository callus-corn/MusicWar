using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class BulletMover : MonoBehaviour ,IObjectMover
{

    private void Start()
    {
        this.OnTriggerEnterAsObservable()
            .Subscribe(_ => Destroy(this.gameObject));
    }

    public void Move(Vector3 velocity)
    {
        this.UpdateAsObservable()
            .Subscribe(_ => transform.position += velocity * Time.deltaTime);
    }	
}
