using System;

[Serializable]
public struct JsonValueGeneric<T>
{
    public T Value;

    public JsonValueGeneric(T value)
    {
        Value = value;
    }
}