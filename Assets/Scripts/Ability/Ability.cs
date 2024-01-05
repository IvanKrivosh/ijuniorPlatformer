using UnityEngine;

public class Ability: MonoBehaviour
{
    public enum States { Enabled, Active, Disabled };

    [SerializeField] private float _duration;
    [SerializeField] private float _rechargeTime;
    [SerializeField] private float _distance;
    [SerializeField] private ParticleSystem _effect;

    public float Duration => _duration;
    public float RechargeTime => _rechargeTime;
    public float Distance => _distance;
    public States State { get; private set; }
    public Enemy Enemy { get; private set; }
    public Player Player { get; private set; }

    private void Start()
    {
        if (_effect != null)
            _effect.Stop();
    }

    public void Init(Player player)
    {
        Player = player;
    }

    public virtual void Activate(Enemy enemy = null)
    {
        Enemy = enemy;

        if (State == States.Enabled)
            SetNextState();
    }

    private void SetNextState()
    {
        switch (State)
        {
            case States.Enabled: SetState(States.Active, _duration); break;
            case States.Active: SetState(States.Disabled,_rechargeTime); break;
            default: SetState(States.Enabled); break;                
        }
    }

    private void SetState(States state, float? time = null)
    {
        State = state;        

        if (time.HasValue)
            Invoke(nameof(SetNextState), time.Value);

        if (State == States.Active && _effect != null)
            _effect.Play();
        else if (State == States.Disabled && _effect != null)            
            _effect.Stop();
    }
}
