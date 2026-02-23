using UnityEngine;
using System;
using System.Collections.Generic;
using HelinTest.OdinSerializer;
using System.Collections.Specialized;
using System.Collections;
using Newtonsoft.Json;
//using UnityEngine.InputSystem;

namespace MultipleClassOdinTestSerializer
{
    public class TestInventoryManager : MonoBehaviour, ISaveable
    {
        [Serializable]
        public class InventorySaveData
        {
            public List<string> items = new List<string>();
            public int gold;

            public Dictionary<string, int> testDictionary = new Dictionary<string, int>();
            public SerializedDictionary<string, int> testSerializedDictionary = new SerializedDictionary<string, int>();

            public DateTime dateTime;
            public SerializedDateTime serializedDateTime = new SerializedDateTime();

            public int[] testIntArray = { 1, 2, 3, 4, 5 };
            public float[][] testNestedFloatArray = new float[][] {
                new float[] { 1.1f, 1.1f },
                new float[] { 1.1f, 1.1f }
            };

            public ArrayList myArrayList = new ArrayList() { 1, "Hello", 3.14f, true };
        }

        [SerializeField]
        private InventorySaveData saveData = new InventorySaveData();
        
     
        public string GetSaveKey() => "InventoryManager";
        public int testIndex = 0;
        ///////////////////////////////////////////////////////////////////////////
        /// Save using byte array
        public byte[] SaveWithByteArray()
        {
            return SerializationUtility.SerializeValue(saveData, DataFormat.JSON);
        }

        public void LoadWithByteArray(byte[] state)
        {
            saveData = SerializationUtility.DeserializeValue<InventorySaveData>(state, DataFormat.JSON);
        }

        ///////////////////////////////////////////////////////////////////////////
        /// Save using object
        public object SaveWithObject()
        {
            return saveData;
        }
        public void LoadWithObject(object state)
        {
            saveData = (InventorySaveData)state;
        }

        ///////////////////////////////////////////////////////////////////////////
        // Save using unity json
        public string SaveWithJson()
        {
            saveData.serializedDateTime.DateTime = DateTime.Now;

            saveData.testSerializedDictionary.Add("key" + testIndex, testIndex);
            testIndex++;

            return JsonUtility.ToJson(saveData);
        }

        public void LoadWithJson(string json)
        {
            saveData = JsonUtility.FromJson<InventorySaveData>(json);
        }

        ///////////////////////////////////////////////////////////////////////////
        // Save using newtonsoft json

        public string SaveWithNewtonSoftJson()
        {
            return JsonConvert.SerializeObject(saveData, Formatting.Indented);
        }
        public void LoadWithNewtonSoftJson(string json)
        {
            saveData = JsonConvert.DeserializeObject<InventorySaveData>(json);
        }
    }
}

