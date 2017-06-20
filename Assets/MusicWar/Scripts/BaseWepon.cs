using UnityEngine;
using UniRx;
using UniRx.Triggers;

public abstract class BaseWepon : MonoBehaviour, IBulletUsable, IHitable
{
    public IReadOnlyReactiveProperty<float> Bullets { get { return Magazine; } }
    public float MaxBullets { get { return MaxBullet; } }

    protected abstract ReactiveProperty<float> Magazine { get; }

    protected abstract GameObject Bullet { get; }
    protected abstract GameObject User { get; }
    protected abstract IStateProvider UserState { get; }
    protected abstract Damage BulletDamage { get; }
    protected abstract Damage ClubDamage { get; }
    protected abstract Vector3 BulletVelocity { get; }
    protected abstract float UseBulletAmount { get; }
    protected abstract float ChargeBulletAmount { get; }
    protected abstract float MaxBullet { get; }

    protected WeponMode _weponMode = WeponMode.Club;

    public void UseBullet()
    {
        if (Magazine.Value >= UseBulletAmount)
        {
            Magazine.Value -= UseBulletAmount;

            var bullet = Instantiate<GameObject>(Bullet);
            var bulletMover = bullet.GetComponent<IMovable>();
            var damageApplyer = bullet.GetComponent<DamageApplyer>();
            bullet.transform.position = transform.position + BulletVelocity*Time.deltaTime;
            damageApplyer.Value = BulletDamage;
            bulletMover.Move(BulletVelocity);
        }
    }

    public void ChargeBullet()
    {
        if (Magazine.Value + ChargeBulletAmount <= MaxBullet)
        {
            Magazine.Value += ChargeBulletAmount;
        }
    }

    public void Hit()
    {
        this.OnTriggerEnterAsObservable()
            .Where(_ => _weponMode == WeponMode.Club )
            .Where(_ => UserState.IsAttacking() )
            .Where(collision => collision.gameObject.GetComponent<IDamageAppliable>() != null)
            .Select(collision => collision.gameObject.GetComponent<IDamageAppliable>())
            .Subscribe(collision => collision.ApplyDamage(ClubDamage));
    }

    public void UseWepon()
    {
        switch (_weponMode)
        {
            case WeponMode.Gun:
                UseBullet();
                break;
            case WeponMode.Club:
                Hit();
                break;
            default:
                break;
        }
    }

    public void ChangeMode()
    {
        switch (_weponMode)
        {
            case WeponMode.Gun:
                _weponMode = WeponMode.Club;
                break;
            case WeponMode.Club:
                _weponMode = WeponMode.Gun;
                break;
            default:
                break;
        }
    }
}
