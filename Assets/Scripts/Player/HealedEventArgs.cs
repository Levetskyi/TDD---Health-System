using System;

public class HealedEventArgs : EventArgs
{
    public int Amount { get; private set; }

    public HealedEventArgs(int amount)
    {
        Amount = amount;
    }
}