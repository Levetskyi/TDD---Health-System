using UnityEngine;

public class Troll : MonoBehaviour, IEnemy
{
    private readonly Enemy _enemy = new(10, 10);

    [ContextMenu("DealDamage")]
    public void DealDamage()
    {
        EventBus<DamageEvent>.Raise(new DamageEvent { DamageAmount = 1});
    }

    public void TakeDamage(int amount)
    {
        _enemy.TakeDamage(amount);

        if (_enemy.CurrentHealth == 0)
            Die();
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}