using UnityEngine;

public class PlayerWrapper : MonoBehaviour
{
    private EventBinding<HealEvent> _healEvenetBinding;
    private EventBinding<DamageEvent> _damageEventBinding;

    private readonly Player _player = new(20,20);

    private void OnEnable()
    {
        _healEvenetBinding = new EventBinding<HealEvent>(Heal);
        EventBus<HealEvent>.Register(_healEvenetBinding);

        _damageEventBinding = new EventBinding<DamageEvent>(Damage);
        EventBus<DamageEvent>.Register(_damageEventBinding);
    }

    private void OnDisable()
    {
        EventBus<HealEvent>.Deregister(_healEvenetBinding);
        EventBus<DamageEvent>.Deregister(_damageEventBinding);
    }

    private void Heal(HealEvent healEvent)
    {
        _player.Heal(healEvent.HealAmount);
    }

    private void Damage(DamageEvent damageEvent)
    {
        _player.TakeDamage(damageEvent.DamageAmount);
    }
}