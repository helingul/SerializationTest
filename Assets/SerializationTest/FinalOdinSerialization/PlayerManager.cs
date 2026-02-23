using HelinTest.OdinSerializer;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;


namespace FinalSaveSystem
{
    [Serializable]
    public class PlayerSaveData
    {
        public int health;
        public int level;
    }

    public class PlayerManager : PlayerSaveData, ISaveable
    {
        public void Save(GameSaveData saveData)
        {
            saveData.playerSaveData = this;
        }

        public void LoadData(GameSaveData saveData)
        {
            ReflectionHelper.CopyFields(this, saveData.playerSaveData);

            //health = saveData.playerSaveData.health;
            //level = saveData.playerSaveData.level;
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

