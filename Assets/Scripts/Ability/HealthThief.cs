using System.Collections;
using UnityEngine;

public class HealthThief: Ability
{          
    [SerializeField] private int _healthValue;
    [SerializeField] private float _delaySteal = 0.5f;    

    private WaitForSeconds _waitSeconds;

    private void Awake()
    {
        _waitSeconds = new WaitForSeconds(_delaySteal);        
    }

    public override void Activate(Enemy enemy)
    {
        if (Player != null)
        {
            base.Activate(enemy);
            StartCoroutine(StealHealth());
        }   
    }

    private IEnumerator StealHealth()
    {
        while (State == States.Active && !Player.HasFullHealth)
        {
            Steal();
            yield return _waitSeconds;
        }
    }

    private void Steal()
    {
        if (Enemy == null) return;

        int stolenHealth = Enemy.HealthValue >= _healthValue ? _healthValue : Enemy.HealthValue;

        Enemy.TakeDamage(_healthValue);
        Player.TakeHealing(stolenHealth);
    }

}
