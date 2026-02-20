using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SerializationType
{
    BasicSerialization,
    MultipleClassSerializationWithByteArray,
    MultipleClassSerializationWithObject,
    MultipleClassSerializationWithObject_Compressed,
    MultipleClassSerializationWithObject_Encrpyted,
    UnityJsonSerialization,
    NewtonsoftJsonSerialization,

}

public class SerializationManager : MonoBehaviour
{
    public MultipleClassOdinTestSerializer.MultipleClassSerialization multipleClassSerialization;
    public BasicOdinSerializer.OdinTestSerializer odinTestSerializer;

    public SerializationType serializationType;
    public void Save()
    {
        switch (serializationType)
        {
            case SerializationType.BasicSerialization:
                odinTestSerializer.SaveGame();
                break;

            case SerializationType.MultipleClassSerializationWithByteArray:
                multipleClassSerialization.SaveGameWithByteArray();
                break;
            case SerializationType.MultipleClassSerializationWithObject:
                multipleClassSerialization.SaveGameWithObject();
                break;
            case SerializationType.MultipleClassSerializationWithObject_Compressed:
                multipleClassSerialization.SaveGameWithObject_Compressed();
                break;
            case SerializationType.MultipleClassSerializationWithObject_Encrpyted:
                multipleClassSerialization.SaveGameWithObject_Encrypted();
                break;
            case SerializationType.UnityJsonSerialization:
                multipleClassSerialization.SaveGameWithUnityJson();
                break;
            case SerializationType.NewtonsoftJsonSerialization:
                multipleClassSerialization.SaveGameWithNewtonSoftJson();
                break;
            default:
                break;
        }
    }

    public void Load()
    {
        switch (serializationType)
        {
            case SerializationType.BasicSerialization:
                odinTestSerializer.LoadGame();
                break;

            case SerializationType.MultipleClassSerializationWithByteArray:
                multipleClassSerialization.LoadGameWithByteArray();
                break;
            case SerializationType.MultipleClassSerializationWithObject:
                multipleClassSerialization.LoadGameWithObject();
                break;
            case SerializationType.MultipleClassSerializationWithObject_Compressed:
                multipleClassSerialization.LoadGameWithObject_Compressed();
                break;
            case SerializationType.MultipleClassSerializationWithObject_Encrpyted:
                multipleClassSerialization.LoadGameWithObject_Encrypted();
                break;
            case SerializationType.UnityJsonSerialization:
                multipleClassSerialization.LoadGameWithUnityJson();
                break;
            case SerializationType.NewtonsoftJsonSerialization:
                multipleClassSerialization.LoadGameWithNewtonSoftJson();
                break;
            default:
                break;
        }
    }

}
