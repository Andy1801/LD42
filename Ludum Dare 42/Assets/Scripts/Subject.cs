using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour {

    private LinkedList<Iobserver> allObservers;

    private void Awake()
    {
        allObservers = new LinkedList<Iobserver>();
    }

    public void UpdateObservers(Event type)
    {
        foreach(Iobserver observer in allObservers)
        {
            observer.Notify(type);
        }

    }

    public void addObserver(Iobserver observer)
    {
        allObservers.AddLast(observer);
    }

    
}
