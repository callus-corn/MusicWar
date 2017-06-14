using UniRx;

public interface IBulletUsable
{
    IReadOnlyReactiveProperty<float> Bullets { get ; }

    void UseBullet();
    void ChargeBullet();
}
