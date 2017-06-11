using UnityEngine;
using UniRx;

public class Attacker : MonoBehaviour
{
    private GameObject _bullet;
    private IInputProvider _input;
    private const float _bulletSpped = 5.0f;

    private void Awake()
    {
        _bullet = (GameObject)Resources.Load("Prefabs/Bullet");
        _input = this.GetComponent<IInputProvider>();
    }

    void Start ()
    {
        _input.LeftClick
            .Select(_ => transform.forward * _bulletSpped)
            .Subscribe(velocity => {
                var bullet = Instantiate(_bullet);
                var bulletMover = bullet.GetComponent<IObjectMover>();
                bullet.transform.position = transform.position + transform.forward.normalized*0.3f + new Vector3(0 ,1.0f ,0);
                bulletMover.Move(velocity);
            });
	}
}
