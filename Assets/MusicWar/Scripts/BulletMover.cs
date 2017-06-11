using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class BulletMover : MonoBehaviour ,IObjectMover{

    public void Move(Vector3 velocity)
    {
        this.UpdateAsObservable()
            .Subscribe(_ => transform.position += velocity * Time.deltaTime);
    }	
}
