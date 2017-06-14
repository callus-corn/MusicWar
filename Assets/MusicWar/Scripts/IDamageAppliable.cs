using UniRx;

interface IDamageAppliable
{
    IReadOnlyReactiveProperty<float> HP { get ; }
    void ApplyDamage(Damage damage);
}
