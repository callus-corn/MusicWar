using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerAtacker : MonoBehaviour
{
    private IInputProvider _input;
    private PlayerState _state;
    private BaseWepon _wepon;

    private void Awake()
    {
        _input = this.GetComponent<IInputProvider>();
        _state = this.GetComponent<PlayerState>();
        _wepon = transform.Find("Wepon").gameObject.GetComponent<BaseWepon>();
    }

    private void Start()
    {
        _input.Attack
            .Subscribe(_ => _wepon.UseWepon());

        _input.Charge
            .Subscribe(_ => _wepon.ChargeBullet());

        _input.Change
            .Subscribe(_ => _wepon.ChangeMode());
    }
}
