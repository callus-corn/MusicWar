using UnityEngine;
using UniRx;
using System;

public class PlayerState : MonoBehaviour, IStateProvider
{
    public IReadOnlyReactiveProperty<Status> State { get { return _state; } }

    ReactiveProperty<Status> _state = new ReactiveProperty<Status>(Status.Running);

    public bool IsAttacking(){return _state.Value == Status.Attacking;}
    public bool IsAttackable() { return _state.Value == Status.Running; }

    public void ToDead() { _state.Value = Status.Dead; }
    public void ToAttacking() { _state.Value = Status.Attacking; }
    public void ToChanging() { _state.Value = Status.Changing; }
    public void ToCharging() { _state.Value = Status.Charging; }
    public void ToRunning() { _state.Value = Status.Running; }

    private void Update()
    {
        _state.Where(state => !IsAttackable())
            .Delay(TimeSpan.FromMilliseconds(0.2))
            .Subscribe(steta => _state.Value = Status.Running);
    }
}
