using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializedDateTime : ISerializationCallbackReceiver
{
    [SerializeField]
    private string serializedDateTime;

    private DateTime dateTime;

    public DateTime DateTime
    {
        get => dateTime;
        set => dateTime = value;
    }
    public SerializedDateTime()
    {
        serializedDateTime = null;
        dateTime = default;
    }
    public void OnAfterDeserialize()
    {
        dateTime = DateTime.Parse(serializedDateTime).ToLocalTime();
    }

    public void OnBeforeSerialize()
    {
       serializedDateTime = dateTime.ToUniversalTime().ToString("o");
    }

}
