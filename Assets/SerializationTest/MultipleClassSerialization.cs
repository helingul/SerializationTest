using HelinTest.OdinSerializer;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace MultipleClassOdinTestSerializer
{
    public interface ISaveable
    {
        string GetSaveKey();
        byte[] SaveWithByteArray();
        void LoadWithByteArray(byte[] state);

        object SaveWithObject();
        void LoadWithObject(object state);

        string SaveWithJson(); 
        void LoadWithJson(string json);

        string SaveWithNewtonSoftJson();
        void LoadWithNewtonSoftJson(string json);
    }


    public class MultipleClassSerialization : MonoBehaviour
    {
        private List<ISaveable> saveables = new List<ISaveable>();
        private string filePath;

        private void Awake()
        {
            filePath = Path.Combine(Application.persistentDataPath, "multipleManagerSave.json");

            saveables = FindObjectsByType<MonoBehaviour>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None)
                .OfType<ISaveable>()
                .ToList();
        }

        ///////////////////////////////////////////////////////////////////////////
        /// Save using byte array

        public void SaveGameWithByteArray()
        {
            Dictionary<string, byte[]> state = new Dictionary<string, byte[]>();

            foreach (var saveable in saveables)
            {
                state[saveable.GetSaveKey()] = saveable.SaveWithByteArray();
            }

            byte[] bytes = SerializationUtility.SerializeValue(state, DataFormat.JSON);
            File.WriteAllBytes(filePath, bytes);

            Debug.Log("Game Saved!");
        }

        public void LoadGameWithByteArray()
        {
            byte[] bytes = File.ReadAllBytes(filePath);

            var state = SerializationUtility
                .DeserializeValue<Dictionary<string, byte[]>>(bytes, DataFormat.JSON);

            foreach (var saveable in saveables)
            {
                if (state.TryGetValue(saveable.GetSaveKey(), out var savedState))
                {
                    saveable.LoadWithByteArray(savedState);
                }
            }

            Debug.Log("Game Loaded!");
        }


        ///////////////////////////////////////////////////////////////////////////
        /// Save using object
        public void SaveGameWithObject()
        {

            List<object> state = new List<object>();

            foreach (var saveable in saveables)
            {
                state.Add(saveable.SaveWithObject());
            }

            byte[] bytes = SerializationUtility.SerializeValue(state, DataFormat.JSON);
            File.WriteAllBytes(filePath, bytes);

            Debug.Log("Game Saved!");
        }

        public void LoadGameWithObject()
        {
            byte[] bytes = File.ReadAllBytes(filePath);

            var state = SerializationUtility
                .DeserializeValue<List<object>>(bytes, DataFormat.JSON);

            for(int i = 0 ; i < saveables.Count; ++i)
            {
                saveables[i].LoadWithObject(state[i]);
            }

            Debug.Log("Game Loaded!");
        }


        ///////////////////////////////////////////////////////////////////////////
        /// Save using object - compressed version
        public void SaveGameWithObject_Compressed()
        {

            Dictionary<string, object> state = new Dictionary<string, object>();

            foreach (var saveable in saveables)
            {
                state[saveable.GetSaveKey()] = saveable.SaveWithObject();
            }

            byte[] bytes = SerializationUtility.SerializeValue(state, DataFormat.JSON);

            bytes = Utilities.Compress(bytes);

            File.WriteAllBytes(filePath, bytes);

            Debug.Log("Game Saved!");
        }

        public void LoadGameWithObject_Compressed()
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            bytes = Utilities.Decompress(bytes);

            var state = SerializationUtility
                .DeserializeValue<Dictionary<string, object>>(bytes, DataFormat.JSON);

            foreach (var saveable in saveables)
            {
                if (state.TryGetValue(saveable.GetSaveKey(), out var savedState))
                {
                    saveable.LoadWithObject(savedState);
                }
            }

            Debug.Log("Game Loaded!");
        }

        ///////////////////////////////////////////////////////////////////////////
        /// Save using object - encrypted version
        public void SaveGameWithObject_Encrypted()
        {

            Dictionary<string, object> state = new Dictionary<string, object>();

            foreach (var saveable in saveables)
            {
                state[saveable.GetSaveKey()] = saveable.SaveWithObject();
            }

            byte[] bytes = SerializationUtility.SerializeValue(state, DataFormat.JSON);

            bytes = Utilities.Encrypt(bytes);

            File.WriteAllBytes(filePath, bytes);

            Debug.Log("Game Saved!");
        }

        public void LoadGameWithObject_Encrypted()
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            bytes = Utilities.Decrypt(bytes);

            var state = SerializationUtility
                .DeserializeValue<Dictionary<string, object>>(bytes, DataFormat.JSON);

            foreach (var saveable in saveables)
            {
                if (state.TryGetValue(saveable.GetSaveKey(), out var savedState))
                {
                    saveable.LoadWithObject(savedState);
                }
            }

            Debug.Log("Game Loaded!");
        }

        ///////////////////////////////////////////////////////////////////////////
        /// Save using unity json utility
        /// 

        // We need to wrap the list in order to save the list
        [Serializable]
        public class SaveableListWrapper
        {
            public List<string> states;

            public SaveableListWrapper(List<string> states)
            {
                this.states = states;
            }
        }

        public void SaveGameWithUnityJson()
        {
            List<string> jsonStates = new List<string>();

            foreach (var saveable in saveables)
            {
                jsonStates.Add(saveable.SaveWithJson());
            }
         
            SaveableListWrapper wrapper = new SaveableListWrapper(jsonStates);

            string json = JsonUtility.ToJson(wrapper);
            File.WriteAllText(filePath, json);

            Debug.Log("Game Saved with Unity Json");
        }

        public void LoadGameWithUnityJson()
        {
            string json = File.ReadAllText(filePath);

            SaveableListWrapper wrapper = JsonUtility.FromJson<SaveableListWrapper>(json);

            if (wrapper == null || wrapper.states == null)
            {
                Debug.LogWarning("Save file is empty or corrupted!");
                return;
            }

            List<string> jsonStates = wrapper.states;

            for (int i = 0; i < saveables.Count; i++)
            {
                if (i >= jsonStates.Count)
                    break;

                saveables[i].LoadWithJson(jsonStates[i]);
            }

        }


        // This version is the mostly same as the upper functions but instead of holding SaveableListWrapper it uses SaveableDictionaryWrapper 

        //[Serializable]
        //public class SaveableItem
        //{
        //    public string key;
        //    public string value;

        //    public SaveableItem(string key, string value)
        //    {
        //        this.key = key;
        //        this.value = value;
        //    }
        //}

        //[Serializable]
        //private class SaveableDictionaryWrapper
        //{
        //    public List<SaveableItem> states;

        //    public SaveableDictionaryWrapper(Dictionary<string, string> states)
        //    {
        //        this.states = new List<SaveableItem>();

        //        // Convert the dictionary to a List<SaveableItem>
        //        foreach (var pair in states)
        //        {
        //            // Instead of adding to the dictionary, add to the list of SaveableItem
        //            this.states.Add(new SaveableItem(pair.Key, pair.Value));
        //        }
        //    }
        //}


        //public void SaveGameWithUnityJson()
        //{
        //    // JSON object içinde tüm nesnelerin verisini tutacağız
        //    Dictionary<string, string> jsonStates = new Dictionary<string, string>();

        //    foreach (var saveable in saveables)
        //    {
        //        // Her saveable nesnesinin JSON string'ini alıyoruz
        //        jsonStates[saveable.GetSaveKey()] = saveable.SaveWithJson();
        //    }

        //    // JSON object olarak tüm verileri birleştiriyoruz ve dosyaya kaydediyoruz
        //    string json = JsonUtility.ToJson(new SaveableDictionaryWrapper(jsonStates));
        //    File.WriteAllText(filePath, json);

        //    Debug.Log("Game Saved with Unity Json!");
        //}

        //public void LoadGameWithUnityJson()
        //{
        //    // JSON dosyasını oku
        //    string json = File.ReadAllText(filePath);

        //    // JSON objesini çözümleyelim
        //    var wrapper = JsonUtility.FromJson<SaveableDictionaryWrapper>(json);
        //    var jsonStates = wrapper.states;

        //    // JSON'dan gelen verileri her saveable nesnesine yükleyelim
        //    foreach (var saveable in saveables)
        //    {
        //        // Find the correct saved state by comparing keys
        //        var saveState = jsonStates.Find(item => item.key == saveable.GetSaveKey());

        //        if (saveState != null)
        //        {
        //            saveable.LoadWithJson(saveState.value);
        //        }
        //    }

        //    Debug.Log("Game Loaded with Unity Json!");
        //}



        ///////////////////////////////////////////////////////////////////////////
        /// Save using newton soft json
        /// 

    
        public void SaveGameWithNewtonSoftJson()
        {
            List<string> jsonStates = new List<string>();

            foreach (var saveable in saveables)
            {
                jsonStates.Add(saveable.SaveWithNewtonSoftJson());
            }

            string json = JsonConvert.SerializeObject(jsonStates, Formatting.Indented);
            File.WriteAllText(filePath, json);

            Debug.Log("Game Saved with Unity Json");
        }

        public void LoadGameWithNewtonSoftJson()
        {
            string json = File.ReadAllText(filePath);
            List<string> jsonStates = JsonConvert.DeserializeObject<List<string>>(json);

            if (jsonStates == null)
            {
                Debug.LogWarning("Save file is empty or corrupted!");
                return;
            }

            for (int i = 0; i < saveables.Count; i++)
            {
                if (i >= jsonStates.Count)
                    break;

                saveables[i].LoadWithNewtonSoftJson(jsonStates[i]);
            }

        }

    }
}
