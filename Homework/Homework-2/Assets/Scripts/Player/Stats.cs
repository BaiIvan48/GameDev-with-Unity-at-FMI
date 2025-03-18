using System;
using UnityEngine;

//I didn't reinvent the wheel.
//I had originally implemented the key and heart system differently,
//but after watching the video "2022 Practice 2,"
//where you mentioned that this is the correct approach,
//I changed it.

public class Stats<T> : MonoBehaviour
{
    public event Action<T> valueUpdateNotify;
    private T value;

    public void setValue(T value)
    {
        this.value = value;
        valueUpdateNotify?.Invoke(value);
    }

    public T getValue()
    {
        return value;
    }
}
