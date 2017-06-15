using UniRx;

public interface IStateProvider
{
    IReadOnlyReactiveProperty<Status> State { get; }

    bool IsAttacking();
    bool IsAttackable();

    void ToDead();
    void ToAttacking();
    void ToChanging();
    void ToCharging();
    void ToRunning();
}
