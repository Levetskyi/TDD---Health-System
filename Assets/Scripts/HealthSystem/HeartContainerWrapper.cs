using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HeartContainerWrapper : MonoBehaviour
{
    [SerializeField] private List<HeartWrapper> _hearts = new();

    private EventBinding<HealEvent> _healEventBinding;
    private EventBinding<DamageEvent> _damageEventBinding;

    private HeartContainer _heartContainer;

    private void Awake()
    {
        _heartContainer = new HeartContainer(
            _hearts.Select(heart => heart.GetHeart()).ToList());
    }

    private void OnEnable()
    {
        _healEventBinding = new EventBinding<HealEvent>(_heartContainer.Replenish);
        EventBus<HealEvent>.Register(_healEventBinding);

        _damageEventBinding = new EventBinding<DamageEvent>(_heartContainer.Deplete);
        EventBus<DamageEvent>.Register(_damageEventBinding);
    }

    private void OnDisable()
    {
        EventBus<DamageEvent>.Deregister(_damageEventBinding);
        EventBus<HealEvent>.Deregister(_healEventBinding);
    }
}