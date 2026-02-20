using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> keys = new List<TKey>();

    [SerializeField]
    private List<TValue> values = new List<TValue>();

    public void OnAfterDeserialize()
    {
        Clear();

        if (keys.Count != values.Count)
        {
            throw new Exception("Number of keys and values does not match");
        }

        for (int i = 0; i < keys.Count; i++)
        {
            this[keys[i]] = values[i];
        }
    }

    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();

        foreach (var pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

}

///////////////////////////////////////////////////////////

//[Serializable]
//public class SerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
//{
//    [SerializeField]
//    private List<SerializedDictionaryElement<TKey, TValue>> elements = new List<SerializedDictionaryElement<TKey, TValue>>();

//    public void OnAfterDeserialize()
//    {
//        Clear();

//        foreach (var element in elements)
//        {
//            this[element.key] = element.value;
//        }
//    }

//    public void OnBeforeSerialize()
//    {
//        elements.Clear();

//        foreach (var pair in this)
//        {
//            elements.Add(new SerializedDictionaryElement<TKey, TValue>(pair.Key, pair.Value));
//        }
//    }

//}