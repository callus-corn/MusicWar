using UnityEngine;
using UniRx;

public class Wepon : MonoBehaviour
{
    private GameObject _bullet;
    private IInputProvider _input;
    private IStateProvider _player;
    private IBulletUsable _user;

    private const float _bulletSpped = 5.0f;
    private const float _use = 1;
    private const float _damage = 40;
    private const float _max = 10;
    private const float _charge = 1;

    private void Awake()
    {
        _bullet = (GameObject)Resources.Load("Prefabs/Bullet");
        _input = this.GetComponent<IInputProvider>();
        _player = this.GetComponent<IStateProvider>();
        _user = this.GetComponent<IBulletUsable>();
    }

    void Start ()
    {
        _input.Attack
            .Where(_ => _player.Magazine.Value >= _use )
            .Select(_ => transform.forward * _bulletSpped)
            .Subscribe(velocity => {
                _user.UseBullet(_use);

                var bullet = Instantiate(_bullet);
                var bulletMover = bullet.GetComponent<IObjectMover>();
                var bulletDamage = bullet.GetComponent<IDamage>();
                bullet.transform.position = transform.position + transform.forward.normalized*0.3f + new Vector3(0 ,1.0f ,0);
                bulletDamage.Value = _damage;
                bulletDamage.Attacker = _player;
                bulletMover.Move(velocity);
            });

        _input.Charge
            .Where(_ => _player.Magazine.Value <= _max)
            .Subscribe(_ => _user.ChargeBullet(_charge));
	}
}
