using System;

public class DamagedEventArgs : EventArgs
{
    public int Amount { get; private set; }

    public DamagedEventArgs(int amount)
    {
        Amount = amount;
    }
}