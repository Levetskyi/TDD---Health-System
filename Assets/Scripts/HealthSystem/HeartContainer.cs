using System.Collections.Generic;
using System.Linq;
using System;

public class HeartContainer
{
    private readonly List<Heart> _hearts;

    public HeartContainer(List<Heart> hearts)
    {
        _hearts = hearts;
    }

    public void Replenish(HealEvent healEvent)
    {
        foreach (var heart in _hearts)
        {
            var toReplenish = healEvent.HealAmount < heart.EmptyHeartPieces
                ? healEvent.HealAmount
                : heart.EmptyHeartPieces;

            healEvent.HealAmount -= heart.EmptyHeartPieces;
            heart.Replenish(toReplenish);

            if (healEvent.HealAmount <= 0)  
                break;
        }
    }

    public void Deplete(DamageEvent damageEvent)
    {
        foreach(var heart in _hearts.AsEnumerable().Reverse())
        {
            var toDeplete = damageEvent.DamageAmount < heart.FilledHeartPieces
                ? damageEvent.DamageAmount
                : heart.FilledHeartPieces;

            damageEvent.DamageAmount -= heart.FilledHeartPieces;
            heart.Deplete(toDeplete); 

            if (damageEvent.DamageAmount <= 0)
                break;
        }
    }
}