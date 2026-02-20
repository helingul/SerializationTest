using UnityEngine;
using System;
using HelinTest.OdinSerializer;
using static MultipleClassOdinTestSerializer.TestInventoryManager;
using Newtonsoft.Json;

namespace MultipleClassOdinTestSerializer
{
    public class TestPlayerManager : MonoBehaviour, ISaveable
    {
        [Serializable]
        public class PlayerSaveData
        {
            public int health;
            public int level;
        }

        [SerializeField]
        private PlayerSaveData saveData = new PlayerSaveData();

        private void Start()
        {
            saveData.health = 100;
            saveData.level = 1;
        }

        public string GetSaveKey() => "PlayerManager";

        ///////////////////////////////////////////////////////////////////////////
        // Save using byte array 
        public byte[] SaveWithByteArray()
        {
            return SerializationUtility.SerializeValue(saveData, DataFormat.Binary);
        }

        public void LoadWithByteArray(byte[] state)
        {
            saveData = SerializationUtility.DeserializeValue<PlayerSaveData>(state, DataFormat.Binary);
            Debug.Log("Loaded Player Health: " + saveData.health);
        }


        ///////////////////////////////////////////////////////////////////////////
        /// Save using object
        public object SaveWithObject()
        {
            return saveData;
        }
        public void LoadWithObject(object state)
        {
            saveData = (PlayerSaveData)state;
        }


        ///////////////////////////////////////////////////////////////////////////
        // Save using unity json
        public string SaveWithJson()
        {
            return JsonUtility.ToJson(saveData);
        }

        public void LoadWithJson(string json)
        {
            saveData = JsonUtility.FromJson<PlayerSaveData>(json);
        }



        ///////////////////////////////////////////////////////////////////////////
        // Save using newtonsoft json


        public string SaveWithNewtonSoftJson()
        {
            return JsonConvert.SerializeObject(saveData, Formatting.Indented);
        }
        public void LoadWithNewtonSoftJson(string json)
        {
            saveData = JsonConvert.DeserializeObject<PlayerSaveData>(json);
        }
    }

}
