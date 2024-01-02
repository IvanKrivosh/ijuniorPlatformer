using UnityEngine;

public class Health
{   
    public delegate void HealthHandler();
    public event HealthHandler Died;
    public event HealthHandler ChandedHealth;    

    public Health(int maxValue, int? currentValue = null)
    {
        MaxValue = maxValue;

        ChangeHealth(currentValue.HasValue ? currentValue.Value : maxValue);
    }

    public int Value { get; private set; }
    public int MaxValue { get; private set; }

    public bool IsAlive => Value > 0;

    public void TakeDamage(int damage)
    {
        int negativeCoefficient = -1;

        if (!IsAlive) return;                

        ChangeHealth(damage * negativeCoefficient);
    }

    public void TakeHealing(int healing)
    {
        if (Value == MaxValue || !IsAlive) return;

        ChangeHealth(healing);
    }

    private void ChangeHealth(int health)
    {
        Value = Mathf.Clamp(Value + health, 0, MaxValue);

        ChandedHealth?.Invoke();

        if (Value == 0)
            Died?.Invoke();
    }
}
