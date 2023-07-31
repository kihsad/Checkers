using System.Threading;
using System;
using System.Collections.Generic;

/*
public class GameMonitor : IObservable<GameManager>
{
    List<IObserver<GameManager>> observers;

    public GameMonitor()
    {
        observers = new List<IObserver<GameManager>>();
    }

    private class Unsubscriber : IDisposable
    {
        private List<IObserver<GameManager>> _observers;
        private IObserver<GameManager> _observer;

        public Unsubscriber(List<IObserver<GameManager>> observers, IObserver<GameManager> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (!(_observer == null)) _observers.Remove(_observer);
        }
    }

    public IDisposable Subscribe(IObserver<GameManager> observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);

        return new Unsubscriber(observers, observer);
    }

    public void GetTemperature()
    {
        // Create an array of sample data to mimic a temperature device.
        Nullable<Decimal>[] temps = {14.6m, 14.65m, 14.7m, 14.9m, 14.9m, 15.2m, 15.25m, 15.2m,
                                   15.4m, 15.45m, null };
        // Store the previous temperature, so notification is only sent after at least .1 change.
        Nullable<Decimal> previous = null;
        bool start = true;

        foreach (var temp in temps)
        {
            System.Threading.Thread.Sleep(2500);
            if (temp.HasValue)
            {
                if (start || (Math.Abs(temp.Value - previous.Value) >= 0.1m))
                {
                    GameManager tempData = new GameManager(temp.Value, DateTime.Now);
                    foreach (var observer in observers)
                        observer.OnNext(tempData);
                    previous = temp;
                    if (start) start = false;
                }
            }
            else
            {
                foreach (var observer in observers.ToArray())
                    if (observer != null) observer.OnCompleted();

                observers.Clear();
                break;
            }
        }
    }

}
*/