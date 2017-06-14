using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public abstract class BaseWepon : MonoBehaviour, IBulletUsable, IHitable
{
    public IReadOnlyReactiveProperty<float> Bullets { get { return Magazine; } }

    protected abstract ReactiveProperty<float> Magazine { get; }

    protected abstract GameObject Bullet { get; }
    protected abstract GameObject User { get; }
    protected abstract PlayerState UserState { get; }
    protected abstract Damage BulletDamage { get; }
    protected abstract Damage ClubDamage { get; }
    protected abstract Vector3 BulletVelocity { get; }
    protected abstract float UseBulletAmount { get; }
    protected abstract float ChargeBulletAmount { get; }
    protected abstract float MaxBullet { get; }

    protected WeponMode _weponMode = WeponMode.Club;

    protected abstract Subject<bool> Collider { get; }

    public void UseBullet()
    {
        if (Magazine.Value >= UseBulletAmount)
        {
            Magazine.Value -= UseBulletAmount;

            var bullet = Instantiate<GameObject>(Bullet);
            var bulletMover = bullet.GetComponent<IObjectMovable>();
            var bulletDamage = bullet.GetComponent<BulletDamage>();
            bullet.transform.position = transform.position + BulletVelocity*Time.deltaTime;
            bulletDamage.Value = BulletDamage;
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
        Collider.OnNext(true);
        Observable.Timer(TimeSpan.FromSeconds(10))
            .Subscribe(_ => Collider.OnNext(false));

        this.OnTriggerEnterAsObservable()
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
