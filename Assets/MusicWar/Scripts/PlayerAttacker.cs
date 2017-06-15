using UnityEngine;
using UniRx;

public class PlayerAttacker : MonoBehaviour
{
    private IInputProvider _input;
    private IStateProvider _state;
    private BaseWepon _wepon;

    private void Awake()
    {
        _input = this.GetComponent<IInputProvider>();
        _state = this.GetComponent<IStateProvider>();
        _wepon = transform.Find("Wepon").gameObject.GetComponent<BaseWepon>();
    }

    private void Start()
    {
        _input.Attack
            .Where(_ => _state.IsAttackable() )
            .Subscribe(_ =>{
                _state.ToAttacking();
                _wepon.UseWepon();
            });

        _input.Charge
            .Subscribe(_ => {
                _state.ToCharging();
                _wepon.ChargeBullet();
            });

        _input.Change
            .Subscribe(_ => {
                _state.ToChanging();
                _wepon.ChangeMode();
            });
    }
}
