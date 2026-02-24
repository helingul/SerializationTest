using FinalSaveSystem;
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
    TestSerializerWithOdin,
    MemoryPack,

}

public class SerializationManager : MonoBehaviour
{
    public MultipleClassOdinTestSerializer.MultipleClassSerialization multipleClassSerialization;
    public BasicOdinSerializer.OdinTestSerializer odinTestSerializer;
    public FinalSaveSystem.SaveManager saveManager;

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
            case SerializationType.TestSerializerWithOdin:
                saveManager.SaveGame();
                break;
            case SerializationType.MemoryPack:
                saveManager.SaveGameWithMemoryPack();
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
            case SerializationType.TestSerializerWithOdin:
                saveManager.LoadGame();
                break;
            case SerializationType.MemoryPack:
                saveManager.LoadGameWithMemoryPack();
                break;
            default:
                break;
        }
    }

}
