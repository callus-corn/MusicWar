using UniRx;

public interface IStateProvider
{
    IReadOnlyReactiveProperty<float> HP { get; }
    IReadOnlyReactiveProperty<float> Magazine { get; }
}
