﻿using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Wepon : BaseWepon
{
    protected override ReactiveProperty<float> Magazine{ get { return _magazine; }}
    protected override GameObject Bullet { get { return _bullet; } }
    protected override GameObject User { get { return _user; } }
    protected override IStateProvider UserState { get { return _userState; } }
    protected override Damage BulletDamage { get { return _bulletDamage; } }
    protected override Damage ClubDamage { get { return _clubDamage; } }
    protected override Vector3 BulletVelocity { get { return _bulletVelocity; } }
    protected override float UseBulletAmount { get{ return _useBulletAmount; } }
    protected override float ChargeBulletAmount { get{ return _chargeBulletAmount; } }
    protected override float MaxBullet { get{ return _maxBullet; } }

    private ReactiveProperty<float> _magazine = new ReactiveProperty<float>(_maxBullet);
    private GameObject _bullet;
    private GameObject _user;
    private IStateProvider _userState;
    private Damage _bulletDamage = new Damage(40);
    private Damage _clubDamage = new Damage(100);
    private Vector3 _bulletVelocity;
    private const float _useBulletAmount = 1;
    private const float _chargeBulletAmount = 1;
    private const float _maxBullet = 10;

    private const float _bulletSpped = 10.0f;
    private GameObject _camera;

    private void Awake()
    {
        _bullet = Resources.Load("Prefabs/Bullet") as GameObject;
        _user = transform.root.gameObject;
        _userState = _user.GetComponent<IStateProvider>();
        _camera = User.transform.Find("PlayerCamera").gameObject;
        _bulletVelocity = _camera.transform.forward * _bulletSpped;
    }

    private void Start()
    {
        this.UpdateAsObservable()
            .Subscribe(_ => _bulletVelocity = _camera.transform.forward * _bulletSpped);
    }
}
