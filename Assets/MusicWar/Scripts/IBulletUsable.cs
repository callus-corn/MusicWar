using UniRx;

public interface IBulletUsable
{
    IReadOnlyReactiveProperty<float> Bullets { get ; }
    float MaxBullets { get; }

    void UseBullet();
    void ChargeBullet();
}
