using UnityEngine;
using System;

public class Player
{
    public event EventHandler<HealedEventArgs> Healed;
    public event EventHandler<DamagedEventArgs> Damaged;

    public int CurrentHealth { get; private set; }
    public int MaximumtHealth { get; private set; }

    public Player(int currentHealth, int maximumHealth = 12)
    {
        if (currentHealth < 0)
            throw new ArgumentOutOfRangeException(nameof(currentHealth));

        if (currentHealth > maximumHealth)
            throw new ArgumentOutOfRangeException(nameof(currentHealth));

        CurrentHealth = currentHealth;
        MaximumtHealth = maximumHealth;
    }

    public void Heal(int amount)
    {
        var newHealth = Mathf.Min(CurrentHealth + amount, MaximumtHealth);

        Healed?.Invoke(this, new HealedEventArgs(newHealth - CurrentHealth));

        CurrentHealth = newHealth;
    }

    public void Damage(int amount)
    {
        var newHealth = Mathf.Max(CurrentHealth - amount, 0);

        Damaged?.Invoke(this, new DamagedEventArgs(CurrentHealth - newHealth));

        CurrentHealth = newHealth;
    }

    public class HealedEventArgs : EventArgs
    {
        public int Amount { get; private set; }

        public HealedEventArgs(int amount)
        {
            Amount = amount;
        }
    }

    public class DamagedEventArgs : EventArgs
    {
        public int Amount { get; private set; }

        public DamagedEventArgs(int amount)
        {
            Amount = amount;
        }
    }
}