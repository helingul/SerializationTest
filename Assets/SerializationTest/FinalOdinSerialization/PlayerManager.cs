using HelinTest.OdinSerializer;
using System;
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

    }
}

