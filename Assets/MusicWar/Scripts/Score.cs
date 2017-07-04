using UnityEngine;
using UniRx;

public class Score : MonoBehaviour
{
    public IReadOnlyReactiveProperty<int> PlayerScore { get { return _score; } }
    ReactiveProperty<int> _score = new ReactiveProperty<int>();
    IInputProvider _playerInput;

    public void Initialize()
    {
        var manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _playerInput = GameObject.Find(manager.ID).GetComponent<IInputProvider>();

        _playerInput.Attack
            .Subscribe(_ => _score.Value++);

        _playerInput.Charge
            .Subscribe(_ => _score.Value++);

        _score.Value = 0;
    }

}
