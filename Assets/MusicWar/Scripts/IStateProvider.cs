using UniRx;

public interface IStateProvider
{
    bool IsAttacking();
    bool IsAttackable();
    bool IsDead();

    void ToDead();
    void ToAttacking();
    void ToChanging();
    void ToCharging();
    void ToRunning();
}
