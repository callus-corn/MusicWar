using UnityEngine;
using UniRx;
using System;

public class PlayerState : MonoBehaviour, IStateProvider
{
    public Subject<bool> Respown = new Subject<bool>();

    ReactiveProperty<Status> _lifeState = new ReactiveProperty<Status>(Status.Living);
    ReactiveProperty<Status> _attackState = new ReactiveProperty<Status>(Status.Idling);
    ReactiveProperty<Status> _moveState = new ReactiveProperty<Status>(Status.Idling);

    public bool IsAttacking(){return _attackState.Value == Status.Attacking;}
    public bool IsAttackable() { return _attackState.Value != Status.Attacking; }
    public bool IsDead() { return _lifeState.Value == Status.Dead; }

    public void ToDead() { _lifeState.Value = Status.Dead; }
    public void ToAttacking() { _attackState.Value = Status.Attacking; }
    public void ToChanging() { _attackState.Value = Status.Changing; }
    public void ToCharging() { _attackState.Value = Status.Charging; }
    public void ToRunning() { _moveState.Value = Status.Running; }

    public void Initialize()
    {
        _lifeState.Value = Status.Living;
        _attackState.Value = Status.Idling;
        _moveState.Value = Status.Idling;

        _attackState
            .Where(state => !IsAttackable())
            .Delay(TimeSpan.FromMilliseconds(0.2))
            .Subscribe(steta => _attackState.Value = Status.Idling);

        _lifeState
            .Where(_ => IsDead())
            .Delay(TimeSpan.FromMilliseconds(0.2))
            .Subscribe(_ => Respown.OnNext(true));
    }

}
