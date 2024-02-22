using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public event EventHandler<HealedEventArgs> Healed;
    public event EventHandler<DamagedEventArgs> Damaged;

    public int CurrentHealth { get; private set; }
    public int MaximumtHealth { get; private set; }

    private EventBinding<TestEvent> testEventBinding;
    private EventBinding<PlayerEvent> playerEventBinding;

    private void OnEnable()
    {
        testEventBinding = new EventBinding<TestEvent>(HandleTestEvent);
        EventBus<TestEvent>.Register(testEventBinding);

        playerEventBinding = new EventBinding<PlayerEvent>(HandlePlayerEvent);
        EventBus<PlayerEvent>.Register(playerEventBinding);
    }

    private void OnDisable()
    {
        EventBus<TestEvent>.Deregister(testEventBinding);
        EventBus<PlayerEvent>.Deregister(playerEventBinding);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            EventBus<PlayerEvent>.Raise(new PlayerEvent { Health = CurrentHealth });
    }

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

    private void HandleTestEvent()
    {
        Debug.Log("Test event raised");
    }

    private void HandlePlayerEvent(PlayerEvent playerEvent)
    {
        Debug.Log($"Player event raised with: {playerEvent.Health} health");
    }
}