using System;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;
    public event EventHandler OnDead;

    private readonly int _healthMax;
    private int _health;

    public bool IsDead => _health <= 0;

    public HealthSystem(int healthMax)
    {
        _healthMax = healthMax;
        _health = healthMax;
    }

    public float GetHealthNormalized() => (float)_health / _healthMax;

    public void Damage(int amount)
    {
        if (amount <= 0)
            throw new ArgumentException(nameof(amount));

        _health -= amount;
        _health = Math.Clamp(_health, 0, _healthMax);

        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (_health <= 0)
            Die();
    }

    public void Die() => OnDead?.Invoke(this, EventArgs.Empty);

    public void Heal(int amount)
    {
        _health += amount;
        if (_health > _healthMax)
        {
            _health = _healthMax;
        }

        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnHealed?.Invoke(this, EventArgs.Empty);
    }
}