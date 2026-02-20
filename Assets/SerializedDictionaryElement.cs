using System;

[Serializable]
public class SerializedDictionaryElement<TKey, TValue>
{
    public TKey key;
    public TValue value;

    public SerializedDictionaryElement(TKey key, TValue value)
    {
        this.key = key;
        this.value = value;
    }
}
