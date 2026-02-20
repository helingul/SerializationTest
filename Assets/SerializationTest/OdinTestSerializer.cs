using HelinTest.OdinSerializer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Serialization;

namespace BasicOdinSerializer
{
    public class OdinTestSerializer : MonoBehaviour
    {
        [SerializeField]
        private SaveData saveData = new SaveData();

        [Serializable]
        public class SaveData
        {
            //[FormerlySerializedAs("health")]
            public int health = 0;

        }

        string filePath = "";

        public void Awake()
        {

            filePath = Path.Combine(Application.persistentDataPath, "BasicSerialization.dat");
            Debug.Log(filePath);

        }

        public void SaveGame()
        {
            // You can also use DateFormat.JSON if you prefer a human-readable file.
            // Just remember to use the same format in the LoadState method :)

            byte[] bytes = SerializationUtility.SerializeValue(saveData, DataFormat.Binary);
            File.WriteAllBytes(filePath, bytes);

            Debug.Log("Saved health value" + saveData.health);
        }

        public void LoadGame()
        {
            if (!File.Exists(filePath)) return; // No state to load

            byte[] bytes = File.ReadAllBytes(filePath);
            saveData = SerializationUtility.DeserializeValue<SaveData>(bytes, DataFormat.Binary);
            Debug.Log("Loaded health value" + saveData.health + "-------");
        }
    }
}

