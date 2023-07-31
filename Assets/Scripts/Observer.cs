using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class Observer : IObserver<GameManager>
{
    private IDisposable unsubscriber;
    private bool first = true;
    private GameManager last;

    public virtual void Subscribe(IObservable<GameManager> provider)
    {
        unsubscriber = provider.Subscribe(this);
    }
    public virtual void Unsubscribe()
    {
        unsubscriber.Dispose();
    }
    public virtual void OnCompleted()
    {
        Console.WriteLine("Additional temperature data will not be transmitted.");
    }

    public virtual void OnError(Exception error)
    {
        // Do nothing.
    }

    public virtual void OnNext(GameManager value)
    {
        Console.WriteLine("The temperature is {0}°C at {1:g}");
        if (first)
        {
            last = value;
            first = false;
        }
        else
        {
            Console.WriteLine("   Change: {0}° in {1:g}");
        }
    }
}
*/