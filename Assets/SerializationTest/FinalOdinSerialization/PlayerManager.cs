using HelinTest.OdinSerializer;
using MemoryPack;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;


namespace FinalSaveSystem
{
    //[Serializable]
    //[MemoryPackable]
    //public partial class PlayerSaveData
    //{
    //    public int version = 0;
    //    public int health;
    //    public int level;

    //    //[MemoryPackOnDeserialized]
    //    //private void OnDeserialized()
    //    //{
    //    //  if(version > 1)
    //    //    {

    //    //    }
    //    //}
    //}

    //public class PlayerManager : PlayerSaveData, ISaveable
    //{
    //    public void Save(GameSaveData saveData)
    //    {
    //        saveData.playerSaveData = this;
    //    }

    //    public void LoadData(GameSaveData saveData)
    //    {
    //        ReflectionHelper.CopyFields(this, saveData.playerSaveData);

    //        //health = saveData.playerSaveData.health;
    //        //level = saveData.playerSaveData.level;
    //    }

    //    public void UpdateAllValuesForTesting()
    //    {
    //        health = 10000;
    //        level = 99;
    //    }

    //    public void PrintAllValues()
    //    {
    //        Debug.Log("------------PLAYER MANAGER-----------------");
    //        Debug.Log($"health: {health}");
    //        Debug.Log($"level: {level}");
    //    }

    //    public void LoadDataWithMemoryPack(GameSaveDataMemoryPack saveData)
    //    {
    //       ReflectionHelper.CopyFields(this, saveData.playerSaveData);
    //    }

    //    public void SaveWithMemoryPack(GameSaveDataMemoryPack saveData)
    //    {
    //        saveData.playerSaveData = this;

    //    }
    //}

    public class PlayerManager : ISaveable
    {
        public int health;
        public int level;

        public void Save(GameSaveData saveData)
        {
            saveData.playerManager = this;
        }

        public void LoadData(GameSaveData saveData)
        {
            //ReflectionHelper.CopyFields(this, saveData.playerManager);

            health = saveData.playerManager.health;
            level = saveData.playerManager.level;
        }

        public void UpdateAllValuesForTesting()
        {
            health = 10000;
            level = 99;
        }

        public void PrintAllValues()
        {
            Debug.Log("------------PLAYER MANAGER-----------------");
            Debug.Log($"health: {health}");
            Debug.Log($"level: {level}");
        }
    }
}

